using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task3
{
    class JsonReader
    {
        public static List<Note> ReadNotes()
        {
            List<Note> list = new List<Note>();
            if (!File.Exists("notes.json"))
            {
                var f = File.Create("notes.json");

            }
            if (File.ReadAllText("notes.json").Length > 0)
            {
                list = Note.FromJson();
            }
            return list;
        }


    }
}
