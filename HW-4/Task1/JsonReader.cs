using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task1
{
    public class JsonReader
    {
        [JsonPropertyName("primesFrom")]
        public long PrimesFrom { get; set; }

        [JsonPropertyName("primesTo")]
        public long PrimesTo { get; set; }
        public static JsonReader FromJson()
        {
            var jsonString = File.ReadAllText("settings.json");
            var js = JsonSerializer.Deserialize<JsonReader>(jsonString);
            return js;
        }
    
    }
}
