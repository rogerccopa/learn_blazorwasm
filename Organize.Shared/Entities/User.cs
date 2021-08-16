using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Entities
{
    public class User
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required!!")]
        public string Password { get; set; }
    }
}
