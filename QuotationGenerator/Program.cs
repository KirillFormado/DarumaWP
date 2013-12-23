using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace QuotationGenerator
{
    class Program
    {
        class Quoatation
        {
            public string Text { get; set; }
            public string Author { get; set; }
        }

        class Info
        {
            public string FileName { get; set; }
            public string Url { get; set; }
        }

        private static void Main(string[] args)
        {
            const string urlTemplate = @"http://www.genialnee.net/poslovicy-i-pogovorki/{0}";

            var infoList = new List<Info>
            {
                new Info
                {
                    Url = string.Format(urlTemplate, "bogatstvo"),
                    FileName = "Rich"
                },
                new Info
                {
                    Url = string.Format(urlTemplate, "lyubov"),
                    FileName = "Love"
                },
                new Info
                {
                    Url = string.Format(urlTemplate, "smeshnye"),
                    FileName = "Funy"
                },
                new Info
                {
                    Url = string.Format(urlTemplate, "druzhba"),
                    FileName = "Friendship"
                },
                new Info
                {
                    Url = string.Format(urlTemplate, "udacha"),
                    FileName = "Luck"
                },
                new Info
                {
                    Url = string.Format(urlTemplate, "zdorove"),
                    FileName = "Health"
                }
            };

            var webClient = new HtmlWeb();
            webClient.OverrideEncoding = Encoding.GetEncoding("windows-1251");

            foreach (var info in infoList)
            {
                Console.WriteLine("{0} file start", info.FileName);
                GenerateQuoatationFile(webClient, info);
                Console.WriteLine("{0} file end", info.FileName);
                Console.WriteLine("======================================================");
            }
        }

        private static void GenerateQuoatationFile(HtmlWeb webClient, Info info)
        {
            var url = info.Url;
            var resourcePrefix = info.FileName;
            var fileName = info.FileName + ".resx";
            var quotationList = new List<Quoatation>();

            var result = webClient.Load(url);
            ParsePage(url, webClient);

            var aList = result.GetElementbyId("pages").ChildNodes.Where(n => n.Name == "a");
            var pageList = aList.Select(a => a.Attributes["href"].Value.Split('/').Last(p => !string.IsNullOrEmpty(p))).ToList();

            foreach (var page in pageList)
            {
                var newUrl = url + "/" + page;
                quotationList.AddRange(ParsePage(newUrl, webClient));
            }


            using (var resx = new ResXResourceWriter(fileName))
            {
                for (int i = 0; i < quotationList.Count; i++)
                {
                    //var json = JsonConvert.SerializeObject(quotationList[i]);
                    resx.AddResource(string.Format("{0}_{1}", resourcePrefix, i), quotationList[i].Text);
                }
            }
        }

        private static IEnumerable<Quoatation> ParsePage(string url, HtmlWeb webClient)
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
