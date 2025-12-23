namespace api.Models.Rooms
{
    public class Group
    {
        public int GroupChatId { get; set; }
        public string GroupChatName { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
