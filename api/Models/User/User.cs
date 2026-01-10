using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.User
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string IdenticonUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
