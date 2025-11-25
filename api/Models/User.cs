using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        //public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
