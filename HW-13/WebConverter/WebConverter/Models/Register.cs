using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
