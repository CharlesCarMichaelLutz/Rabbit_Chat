namespace api.Models.Rooms
{
    public class Private
    {
        public int PrivateChatId { get; set; }
        public int UserOneId { get; set; }
        public int UserTwoId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
