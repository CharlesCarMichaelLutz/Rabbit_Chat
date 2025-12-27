using api.Models.Rooms;
using api.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace api.Services
{
    public interface IRoomService
    {
        Task<GroupResponse> CreateGroupAsync(GroupRequest request);
        Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request);
        Task<AddUserResponse> AddUserToGroupAsync(AddUserRequest request);
        Task<IEnumerable<Gmsg>> LoadGroup();
        Task<IEnumerable<Pmsg>> LoadPrivate();
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
            //pre seed the first group chat to be the main public room
                //bool isPrivate = false;

                //var roomCount = await _roomRepository.GetGroupCountAsync();

                //if (roomCount > 0)
                //{
                //    isPrivate = true;
                //}

            var newRoom = new Group
            {
                GroupChatName = request.GroupChatName,
                //IsPrivate = isPrivate,
                IsPrivate = false,
                CreatedDate = DateTime.UtcNow
            };

            return await _roomRepository.CreateGroupAsync(newRoom);
        }
        public async Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request)
        {
            //DB checks if private room exists with UNIQUE constraint 

            //if private room exists already return

            //if private room does not exist create in repository

            var newRoom = new Private
            {
                UserOneId = request.UserOneId,
                UserTwoId = request.UserTwoId,
                CreatedDate = DateTime.UtcNow
            };

            //return new private room

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
        public async Task<IEnumerable<Gmsg>> LoadGroup()
        {
            var chatroomList = await _roomRepository.LoadGroupAsync();

            return chatroomList.Select(m => new Gmsg
            {
                MessageId = m.MessageId,
                Text = m.Text,
                UserId = m.UserId,
                GroupChatId = m.GroupChatId,
                CreatedDate = m.CreatedDate,
                IsDeleted = m.IsDeleted,
            });
        }
        public async Task<IEnumerable<Pmsg>> LoadPrivate()
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
            });
        }

    }
}
