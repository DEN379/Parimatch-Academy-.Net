using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebConverter.Models
{
    /// <summary>
    /// Registration model.
    /// </summary>
    public class Register
    {
        /// <summary>
        /// User login.
        /// </summary>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
