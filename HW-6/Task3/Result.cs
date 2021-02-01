using Newtonsoft.Json;
using System;

namespace Task3
{
    class Result
    {
        [JsonProperty("successful")]
        public int Successful { get; }

        [JsonProperty("failed")]
        public int Failed { get; }
        public Result(int success, int fail)
        {
            Successful = success;
            Failed = fail;
        }
    }
}
