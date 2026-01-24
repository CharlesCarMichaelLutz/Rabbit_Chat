using api.Models.Message;
using api.Models.Rooms;
using api.Repositories;

namespace api.Services
{
    public interface IRoomService
    {
        Task<bool> CreateGroup(GroupRequest request);
        Task<bool> CreateChat(PrivateRequest request);
        Task<bool> AddUserToGroup(UserRequest request);
        Task<IEnumerable<MessageResponse>> LoadGroup(int groupId);
        Task<IEnumerable<MessageResponse>> LoadChat(int privateId);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<bool> CreateGroup(GroupRequest request)
        {
            var newRoom = new Group
            {
                GroupChatName = request.GroupChatName,
                CreatedDate = DateTime.UtcNow
            };

            return await _roomRepository.CreateGroup(newRoom);
        }
        public async Task<bool> CreateChat(PrivateRequest request)
        {
            var newRoom = new Private
            {
                UserOneId = request.UserOneId,
                UserTwoId = request.UserTwoId,
                CreatedDate = DateTime.UtcNow
            };

            return await _roomRepository.CreateChat(newRoom);
        }
        public async Task<bool> AddUserToGroup(UserRequest request)
        {
            var addUser = new User
            {
                UserId = request.UserId,
                GroupChatId = request.GroupChatId,
                JoinedAt = DateTime.UtcNow,
            };
            
            return await _roomRepository.AddUserToGroup(addUser);
        }
        public async Task<IEnumerable<MessageResponse>> LoadGroup(int groupId)
        {
            var chatroomList = await _roomRepository.LoadGroup(groupId);

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
        public async Task<IEnumerable<MessageResponse>> LoadChat(int privateId)
        {
            var chatroomList = await _roomRepository.LoadChat(privateId);

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
