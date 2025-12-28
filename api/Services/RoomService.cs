using api.Models.Rooms;
using api.Repositories;

namespace api.Services
{
    public interface IRoomService
    {
        Task<GroupResponse> CreateGroupAsync(GroupRequest request);
        Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request);
        Task<AddUserResponse> AddUserToGroupAsync(AddUserRequest request);
        Task<IEnumerable<Gmsg>> LoadGroup(int groupId);
        Task<IEnumerable<Pmsg>> LoadPrivate(int privateId);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<GroupResponse> CreateGroupAsync(GroupRequest request)
        {
            var newRoom = new Group
            {
                GroupChatName = request.GroupChatName,
                IsPrivate = false,
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
        public async Task<AddUserResponse> AddUserToGroupAsync(AddUserRequest request)
        {
            var addUser = new AddUser
            {
                UserId = request.UserId,
                GroupChatId = request.GroupChatId,
                JoinedAt = DateTime.UtcNow,
            };
            
            return await _roomRepository.AddUserToGroupAsync(addUser);
        }
        public async Task<IEnumerable<Gmsg>> LoadGroup(int groupId)
        {
            var chatroomList = await _roomRepository.LoadGroupAsync(groupId);

            return chatroomList.Select(m => new Gmsg
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                GroupChatId = m.GroupChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            }).ToList();
        }
        public async Task<IEnumerable<Pmsg>> LoadPrivate(int privateId)
        {
            var chatroomList = await _roomRepository.LoadPrivateAsync();

            return chatroomList.Select(m => new Pmsg
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                PrivateChatId = m.PrivateChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            }).ToList();
        }
    }
}
