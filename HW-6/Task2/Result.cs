using Newtonsoft.Json;
using System;

namespace Task2
{
    class Result
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("primes")]
        public int[] Primes { get; set; }

    }
}
