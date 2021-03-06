﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace Task2
{
    class JsonWriter
    {
        public static void Write(Result result)
        {
            try
            {
                var js = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText("result.json", js);
            } catch (Exception)
            {
                Console.WriteLine("Something went wrong with serializing to file");
            }
        }
    }
}
