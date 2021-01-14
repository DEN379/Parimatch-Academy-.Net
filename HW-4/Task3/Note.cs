using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task3
{
    class Note : INote
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        public static void ToJson(JToken json)
        {
            var js = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText("notes.json", js);
        }
        public static void ToJson(List<Note> json)
        {
            var js = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText("notes.json", js);
        }
        public static void ToJson(Note json)
        {
            var js = JsonConvert.SerializeObject(json, Formatting.Indented);
            File.WriteAllText("notes.json", js);
        }
        public static List<Note> FromJson()
        {
            var jsonString = File.ReadAllText("notes.json");
            //JsonTextReader reader = new JsonTextReader(new StreamReader("cache.json"));
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader.Value);
            //}
            var js = JsonConvert.DeserializeObject<List<Note>>(jsonString);

            return js;

        }


        public static Note ShowNote(long id, List<Note> list, bool isShowBody)
        {
            Note n = list.Where(n => n.Id == id).FirstOrDefault();
            if (n == null)
            {
                Console.WriteLine("A note with this id doesn't exist");
                return null;
            }
            Console.WriteLine($"id# {n.Id}. Title: {n.Title}, Create: {n.Date}");
            if(isShowBody) Console.WriteLine(n.Body);
            Console.WriteLine();
            return n;
        }

        
    }

}
