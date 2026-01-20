using api.Models.Rooms;
using api.Repositories;

namespace api.Services
{
    public interface IRoomService
    {
        //Task<GroupResponse> CreateGroupAsync(GroupRequest request);
        Task<bool> CreateGroupAsync(GroupRequest request);
        Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request);
        //Task<UserResponse> AddUserToGroupAsync(UserRequest request);
        Task<bool> AddUserToGroupAsync(UserRequest request);
        Task<IEnumerable<GroupMessage>> LoadGroup(int groupId);
        Task<IEnumerable<PrivateMessage>> LoadPrivate(int privateId);
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
        public async Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request)
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
        public async Task<IEnumerable<GroupMessage>> LoadGroup(int groupId)
        {
            var chatroomList = await _roomRepository.LoadGroupAsync(groupId);

            return chatroomList.Select(m => new GroupMessage
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                UserName = m.UserName,
                GroupChatId = m.GroupChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            });
        }
        public async Task<IEnumerable<PrivateMessage>> LoadPrivate(int privateId)
        {
            var chatroomList = await _roomRepository.LoadPrivateAsync(privateId);

            return chatroomList.Select(m => new PrivateMessage
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                PrivateChatId = m.PrivateChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            });
        }
    }
}
