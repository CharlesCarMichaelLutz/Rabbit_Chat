namespace api.Models.Message
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? GroupChatId { get; set; }
        public int? PrivateChatId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
