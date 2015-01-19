using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace QuotationGenerator
{
    public class Info
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }

    public class Quoatation
    {
        public string Text { get; set; }
        public string Author { get; set; }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            const string urlTemplateRu = @"http://www.genialnee.net/poslovicy-i-pogovorki/{0}";
            const string urlTemplateEn = @"http://www.goodreads.com/quotes/tag/{0}";

            var parserList = new List<ParserBase>
            {
                //new TextParser(new Info
                //{
                //    Path = "bogatstvo",
                //    FileName = "RichRu"
                //}),
                //new TextParser(new Info
                //{
                //    Path = "lyubov",
                //    FileName = "LoveRu"
                //}),
                //     new TextParser(new Info
                //{
                //    Path = "uspekh",
                //    FileName = "LuckRu"
                //}),
                //new TextParser(new Info{
                //    Path = "druzhba",
                //    FileName = "FriendshipRu"
                //}),
                //new TextParser(new Info{
                //    Path = "zdorovie",
                //    FileName = "HealthRu"
                //}),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "love"),
                    FileName = "LoveEn"
                }),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "friendship"),
                    FileName = "FriendshipEn"
                }),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "health"),
                    FileName = "HealthEn"
                }),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "luck"),
                    FileName = "LuckEn"
                }),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "rich"),
                    FileName = "RichEn"
                }),
                new WebParserEn(new Info
                {
                    Path = string.Format(urlTemplateEn, "humor"),
                    FileName = "FunyEn"
                })
                
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "bogatstvo"),
                //    FileName = "Rich"
                //}),
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "lyubov"),
                //    FileName = "Love"
                //}),
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "smeshnye"),
                //    FileName = "Funy"
                //}),
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "druzhba"),
                //    FileName = "Friendship"
                //}),
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "udacha"),
                //    FileName = "Luck"
                //}),
                //new WebParser(new Info
                //{
                //    Path = string.Format(urlTemplate, "zdorove"),
                //    FileName = "Health"
                //})
            };

            //var webClient = new HtmlWeb();
            //webClient.OverrideEncoding = Encoding.GetEncoding("windows-1251");

            foreach (var parser in parserList)
            {
                Console.WriteLine("{0} file start", parser.FileName);
                GenerateQuoatationFile(parser);
                Console.WriteLine("{0} file end", parser.FileName);
                Console.WriteLine("======================================================");
            }

            Console.ReadLine();
        }

        private static void GenerateQuoatationFile(ParserBase parserBase)
        {
            IList<Quoatation> quotationList = parserBase.Parse();

            var resourcePrefix = parserBase.FileName;
            var fileName = parserBase.FileName + ".resx";
            using (var resx = new ResXResourceWriter(fileName))
            {
                for (int i = 0; i < quotationList.Count(); i++)
                {
                    //var json = JsonConvert.SerializeObject(quotationList[i]);
                    resx.AddResource(string.Format("{0}_{1}", resourcePrefix, i), quotationList[i].Text);
                }
            }
        }
    }

    public abstract class ParserBase
    {
        protected readonly Info _info;

        protected ParserBase(Info info)
        {
            _info = info;
        }

        public abstract IList<Quoatation> Parse();
       

        public string FileName
        {
            get { return _info.FileName; }
        }
    }

    public class TextParser : ParserBase
    {
        public TextParser(Info info) : base(info)
        {
        }

        public override IList<Quoatation> Parse()
        {
            var text = QuotRes.ResourceManager.GetString(_info.Path, QuotRes.Culture).Trim();
            var arrQuot = text.Split('/');

            var quoatList = arrQuot.Where(quot => !string.IsNullOrEmpty(quot)).Select(quot => new Quoatation {Text = quot.Trim()}).ToList();

            return quoatList;
        }
    }

    public class WebParserEn : ParserBase
    {
        public WebParserEn(Info info) : base(info)
        {
        }

        public override IList<Quoatation> Parse()
        {
            var webClient = new HtmlWeb();

            var path = _info.Path;

            var quotationList = new List<Quoatation>();

            while (!string.IsNullOrEmpty(path) && quotationList.Count <= 200)
            {
                path = ParsePage(webClient, path, quotationList);
            }

            return quotationList;
        }

        private string ParsePage(HtmlWeb webClient, string url, List<Quoatation> quotationList)
        {
            var result = webClient.Load(url);

            var nodes = result.DocumentNode.SelectNodes(@"//*[@class=""quoteText""]");

            foreach (var node in nodes)
            {
                var text = HttpUtility.HtmlDecode(node.InnerText).Replace("\n", String.Empty).Trim(' ');

                quotationList.Add(new Quoatation
                {
                    //Author = author,
                    Text = text
                });
            }

            var u = new UriBuilder(webClient.ResponseUri.Host);
            HtmlNode next = result.DocumentNode.SelectSingleNode(@"//*[@class=""next_page""]");
            string nextUrl = null;
            if (next != null)
            {
                nextUrl = next.GetAttributeValue("href", string.Empty);
            }

            return string.IsNullOrEmpty(nextUrl) 
                ? string.Empty
                : string.Format("{0}{1}", u, result.DocumentNode.SelectSingleNode(@"//*[@class=""next_page""]").Attributes["href"].Value);
        }
    }

    public class WebParserBase : ParserBase
    {
        public WebParserBase(Info info) : base(info)
        {
        }

        public override IList<Quoatation> Parse()
        {
            var webClient = new HtmlWeb { OverrideEncoding = Encoding.GetEncoding("windows-1251") };

            var path = _info.Path;

            var quotationList = new List<Quoatation>();

            var result = webClient.Load(path);
            //ParsePage(path, webClient);

            var aList = result.GetElementbyId("pages").ChildNodes.Where(n => n.Name == "a");
            var pageList =
                aList.Select(a => a.Attributes["href"].Value.Split('/').Last(p => !string.IsNullOrEmpty(p)))
                    .ToList();

            foreach (var page in pageList)
            {
                var newUrl = path + "/" + page;
                quotationList.AddRange(ParsePage(newUrl, webClient));
            }

            return quotationList;
        }

        private IEnumerable<Quoatation> ParsePage(string url, HtmlWeb webClient)
        {
            var result = webClient.Load(url);
            var nodes = result.DocumentNode.SelectNodes(@"//*[@class=""texttd""]");
            var quotationList = new List<Quoatation>();
            foreach (var node in nodes)
            {
                var text = node.ChildNodes.FirstOrDefault(n => n.Name == "p").InnerText;
                //var author = node.SelectNodes(@".//a[@class=""who""]").FirstOrDefault().InnerText;

                quotationList.Add(new Quoatation
                {
                    //Author = author,
                    Text = text
                });
            }

            return quotationList;
        }
    }
}
