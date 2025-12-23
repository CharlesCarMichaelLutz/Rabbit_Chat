using api.Models.Rooms;

namespace api.Repositories
{
    public interface IRoomRepository
    {
        Task<int> GetGroupCountAsync();
        Task<GroupResponse> CreateGroupAsync(Group group);
        Task<PrivateResponse> CreatePrivateAsync(Private group);
        Task<AddUserResponse> AddUserToGroupAsync(AddUser user);
        Task<List<GroupResponse>> LoadGroupAsync();
    }
    public class RoomRepository : IRoomRepository
    {
        public RoomRepository()
        {
            //DI connection to npgsql DB Instance 
        }
        public async Task<int> GetGroupCountAsync()
        {
            //create logic to check room_id count
            //return int 
            return null;
        }
        public async Task<GroupResponse> CreateGroupAsync(Group group)
        {
            //write SQL query to create a group
            //pass query to Dapper Method
            //return Group Response
            return null;
        }
        public async Task<PrivateResponse> CreatePrivateAsync(Private group)
        {
            //write SQL query to create a private group
            //pass query to Dapper Method
            //return Private Response
            return null;
        }
        public async Task<AddUserResponse> AddUserToGroupAsync(AddUser user)
        {
            //write SQL query to create a private group
            //pass query to Dapper Method
            //return AddUserResponse
            return null;
        }

        public async Task<List<GroupResponse>> LoadGroupAsync()
        {
            return null;
        }
    }
}
