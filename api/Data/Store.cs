using api.Models.Message;
using api.Models.User;

namespace api.Data
{
    public interface IStore
    {
        public  List<User> Users { get; }
        public List<Message> Messages { get; }

    }
    public class Store : IStore
    {
        public List<User> Users { get; } = new List<User>();
        public List<Message> Messages { get; } = new List<Message>();


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

            Messages.Add(
              new Message
              {
                  MessageId = 1,
                  Text = "Aenean auctor gravida sem.",
                  UserId = 1,
                  GroupChatId = 1,
                  PrivateChatId = null,
                  CreatedDate = new DateTime(2025, 3, 12, 14, 30, 22),
               IsDeleted = false
              }
         );
        }
    }
}

