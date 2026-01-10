using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace api.Models.User
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string IdenticonUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
