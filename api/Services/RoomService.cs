using api.Models.Message;
using api.Models.Rooms;
using api.Repositories;

namespace api.Services
{
    public interface IRoomService
    {
        Task<bool> CreateGroupAsync(GroupRequest request);
        Task<bool> CreatePrivateAsync(PrivateRequest request);
        Task<bool> AddUserToGroupAsync(UserRequest request);
        Task<IEnumerable<MessageResponse>> LoadGroup(int groupId);
        Task<IEnumerable<MessageResponse>> LoadPrivate(int privateId);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<bool> CreateGroupAsync(GroupRequest request)
        {
            var newRoom = new Group
            {
                GroupChatName = request.GroupChatName,
                CreatedDate = DateTime.UtcNow
            };

            return await _roomRepository.CreateGroupAsync(newRoom);
        }
        public async Task<bool> CreatePrivateAsync(PrivateRequest request)
        {
            var newRoom = new Private
            {
                UserOneId = request.UserOneId,
                UserTwoId = request.UserTwoId,
                CreatedDate = DateTime.UtcNow
            };

            return await _roomRepository.CreatePrivateAsync(newRoom);
        }
        public async Task<bool> AddUserToGroupAsync(UserRequest request)
        {
            var addUser = new User
            {
                UserId = request.UserId,
                GroupChatId = request.GroupChatId,
                JoinedAt = DateTime.UtcNow,
            };
            
            return await _roomRepository.AddUserToGroupAsync(addUser);
        }
        public async Task<IEnumerable<MessageResponse>> LoadGroup(int groupId)
        {
            var chatroomList = await _roomRepository.LoadGroupAsync(groupId);

            return chatroomList.Select(m => new MessageResponse
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                UserName = m.UserName,
                GroupChatId = m.GroupChatId,
                PrivateChatId = m.PrivateChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            });
        }
        public async Task<IEnumerable<MessageResponse>> LoadPrivate(int privateId)
        {
            var chatroomList = await _roomRepository.LoadPrivateAsync(privateId);

            return chatroomList.Select(m => new MessageResponse
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                UserName = m.UserName,
                GroupChatId = m.GroupChatId,
                PrivateChatId = m.PrivateChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            });
        }
    }
}
