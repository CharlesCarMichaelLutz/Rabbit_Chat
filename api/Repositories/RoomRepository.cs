using api.Data;
using api.Models.Rooms;

namespace api.Repositories
{
    public interface IRoomRepository
    {
        //Task<int> GetGroupCountAsync();
        Task<GroupResponse> CreateGroupAsync(Group group);
        Task<PrivateResponse> CreatePrivateAsync(Private group);
        Task<AddUserResponse> AddUserToGroupAsync(AddUser user);
        Task<IEnumerable<Gmsg>> LoadGroupAsync();
        Task<IEnumerable<Pmsg>> LoadPrivateAsync();
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public RoomRepository(ISqlConnectionFactory connectionFactory)
        {
            //DI connection to npgsql DB Instance 
            _connectionFactory = connectionFactory;
        }

        //public async Task<int> GetGroupCountAsync()
        //{
        //    //create logic to check room_id count
        //    //return int 
        //    return null;
        //}
        public async Task<GroupResponse> CreateGroupAsync(Group group)
        {
            //write SQL query to create a group
            //pass query to Dapper Method
            //return Group Response
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
        public async Task<PrivateResponse> CreatePrivateAsync(Private group)
        {
            //write SQL query to create a private group
            //pass query to Dapper Method
            //return Private Response

            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
        public async Task<AddUserResponse> AddUserToGroupAsync(AddUser user)
        {
            //write SQL query to create a private group
            //pass query to Dapper Method
            //return AddUserResponse

            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }

        public async Task<IEnumerable<Gmsg>> LoadGroupAsync()
        {
            //write SQL query to get group room
            //pass query to Dapper method
            //return list 

            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }

        public async Task<IEnumerable<Pmsg>> LoadPrivateAsync()
        {
            //write SQL query to get private room
            //pass query to Dapper method
            //return list 

            using var connection = await _connectionFactory.CreateConnectionAsync();

            return null;
        }
    }
}
