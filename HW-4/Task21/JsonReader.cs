using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Task21
{
    class JsonReader
    {
        [JsonProperty("r030")]
        public long R030 { get; set; }

        [JsonProperty("txt")]
        public string Txt { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("exchangedate")]
        public string Exchangedate { get; set; }
        public static List<JsonReader> FromJson()
        {
            var jsonString = "";
            try
            {
                jsonString = File.ReadAllText("cache.json");
            }
            catch (Exception)
            {
                Console.WriteLine("Error with reading from the file.");
                return null;
            }

            var js = JsonConvert.DeserializeObject<List<JsonReader>>(jsonString);

            return js;
        }
        public static void ToJson(JToken json)
        {
            
            var js = JsonConvert.SerializeObject(json, Formatting.Indented);
            try
            {
                File.WriteAllText("cache.json", js);
            }
            catch (Exception)
            {
                Console.WriteLine("Error with writing to the file.");
            }
        }
    }
}
