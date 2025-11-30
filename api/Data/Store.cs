using api.Models;

namespace api.Data
{
    public interface IStore
    {
        public  List<User> Users { get; }
    }
    public class Store : IStore
    {
        public List<User> Users { get; } = new List<User>();

        public Store()
        {
            Users.Add(
                 new User 
                 { 
                     UserName = "john_doe_92", 
                     PasswordHash = "A5B3C9D8E1F4G6H2J7K4", 
                     CreatedDate = new DateTime(2023, 5, 12, 14, 30, 22) 
                 }
            );
        }
    }
}

