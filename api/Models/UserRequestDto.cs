using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class UserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
