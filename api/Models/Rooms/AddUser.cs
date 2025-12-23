namespace api.Models.Rooms
{
    public class AddUser
    {
        public int UserId { get; set; }
        public int GroupChatId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
