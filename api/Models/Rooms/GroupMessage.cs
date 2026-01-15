namespace api.Models.Rooms
{
    public class GroupMessage
    {
            public int MessageId { get; set; }
            public string Text { get; set; }
            public int UserId { get; set; }
            public int? GroupChatId { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool? IsDeleted { get; set; }
    }
}
