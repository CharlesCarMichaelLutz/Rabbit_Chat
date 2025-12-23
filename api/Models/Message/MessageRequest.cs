namespace api.Models.Message
{
    public class MessageRequest
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public int? GroupChatId { get; set; }
        public int? PrivateChatId { get; set; }
    }
}
