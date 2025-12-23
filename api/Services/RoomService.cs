using api.Models.Rooms;
using api.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace api.Services
{
    public interface IRoomService
    {
        Task<GroupResponse> CreateGroupAsync(GroupRequest request);
        Task<PrivateResponse> CreatePrivateAsync(PrivateRequest request);
        Task<bool> AddUserToGroupAsync(AddUserRequest request);
        Task<List<GroupResponse>> LoadRoomAsync();
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
        public async Task<List<GroupResponse>> LoadRoomAsync()
        {
            //var chatroom = 
            //return messageList.Select(m => new MessageModel
            //{
            //    Id = m.MessageId,
            //    UserId = m.UserId,
            //    Text = m.Text
            //});

            return null;
        }
    }
}
