using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Settings
    {
        [JsonProperty("primesFrom")]
        public int PrimesFrom { get; set; }

        [JsonProperty("primesTo")]
        public int PrimesTo { get; set; }
    }
}
