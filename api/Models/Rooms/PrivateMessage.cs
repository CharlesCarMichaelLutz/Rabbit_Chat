namespace api.Models.Rooms
{
    public class PrivateMessage
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int? PrivateChatId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
