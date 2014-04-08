using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DarumaDAL.WP.Utils
{
    class Serializer
    {
        public static async Task<string> Serialize<T>(T obj)
        {
            return await Task<string>.Factory.StartNew(() => JsonConvert.SerializeObject(obj));
        }

        public static async Task<T> Deserialize<T>(string str)
        {
            //TODO: remove hack
            if (str.EndsWith("}}"))
            {
                str = str.Remove(str.Length - 1);
            }
            if (str.EndsWith("\"}\"}"))
            {
                str = str.Remove(str.Length - 2);
            }

            return await Task<T>.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(str));
        }
    }
}
