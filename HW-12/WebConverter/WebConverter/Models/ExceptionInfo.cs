using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConverter.Models
{
    /// <summary>
    /// Model info about exception.
    /// </summary>
    public class ExceptionInfo
    {
        /// <summary>
        /// Code of exception.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Exception message.
        /// </summary>
        public string Message { get; set; }
    }
}
