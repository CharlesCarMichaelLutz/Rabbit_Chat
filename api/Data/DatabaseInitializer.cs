using Dapper;

namespace api.Data
{
    public class DatabaseInitializer
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public DatabaseInitializer(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            await connection.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS users
                (
                    user_id SERIAL PRIMARY KEY,
                    username character varying(100) NOT NULL UNIQUE,
                    password_hash character varying(100) NOT NULL,
                    identicon_url character varying(255) NOT NULL,
                    created_at timestamp without time zone NOT NULL,
                    is_admin boolean DEFAULT false,
                );
                
                CREATE TABLE IF NOT EXISTS messages
                (
                    message_id SERIAL PRIMARY KEY ,
                    text text NOT NULL,
                    user_id integer NOT NULL,
                    username character varying(100) NOT NULL,
                    created_at timestamp without time zone NOT NULL,
                    group_chat_id integer,
                    private_chat_id integer,
                    is_deleted boolean,

                    CONSTRAINT group_or_private 
                    CHECK ((group_chat_id IS NOT NULL AND private_chat_id IS NULL) OR
                    (group_chat_id IS NULL AND private_chat_id IS NOT NULL))
                );
                
                CREATE TABLE IF NOT EXISTS group_chats
                (
                    group_chat_id SERIAL PRIMARY KEY,
                    group_chat_name character varying(100) NOT NULL,
                    created_at timestamp without time zone NOT NULL
                );
                
                CREATE TABLE IF NOT EXISTS group_chats_detail
                (
                    group_chat_id integer NOT NULL,
                    user_id integer NOT NULL,
                    joined_at timestamp without time zone NOT NULL
    
                    CONSTRAINT generate_primary_key PRIMARY KEY (user_id, group_chat_id)
                );
                
                CREATE TABLE IF NOT EXISTS private_chats
                (
                    private_chat_id SERIAL PRIMARY KEY,
                    user1_id integer NOT NULL,
                    user2_id integer NOT NULL,
                    created_at timestamp without time zone NOT NULL,
    
                    CONSTRAINT no_duplicates UNIQUE(user1_id, user2_id),
                    CONSTRAINT ensure_user_order CHECK (user1_id < user2_id)
                );
            ");
        }
    }
}
