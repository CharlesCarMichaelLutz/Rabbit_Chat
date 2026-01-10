namespace api.Models.User
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string IdenticonUrl { get; set; }
    }
}
