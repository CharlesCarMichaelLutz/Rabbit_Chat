using api.Models;

namespace api.Data
{
    public interface IStore
    {
        public  List<User> Users { get; }
    }
    public class Store : IStore
    {
        public List<User> Users { get; }

        public Store()
        {
        Users = new List<User>
            {
                new User { UserName = "john_doe_92", PasswordHash = "A5B3C9D8E1F4G6H2J7K4", CreatedDate = new DateTime(2023, 5, 12, 14, 30, 22) },
                new User { UserName = "alice_smith", PasswordHash = "Z9X8W7V6U5T4S3R2Q1P", CreatedDate = new DateTime(2022, 11, 3, 9, 15, 45) },
                new User { UserName = "bob_builder", PasswordHash = "M1N2B3V4C5X6Z7L8K9J", CreatedDate = new DateTime(2024, 1, 18, 22, 5, 10) },
                new User { UserName = "emma_wilson", PasswordHash = "H3G5F7D8S9A2Z1X4C6", CreatedDate = new DateTime(2023, 8, 29, 11, 40, 33) },
                new User { UserName = "charlie_brown", PasswordHash = "P8O7I6U5Y4T3R2E1W0", CreatedDate = new DateTime(2022, 3, 7, 16, 20, 55) },
                new User { UserName = "diana_prince", PasswordHash = "L9K2J4H6G8F0D3S5A7", CreatedDate = new DateTime(2024, 2, 14, 3, 50, 12) },
                new User { UserName = "frank_sinatra", PasswordHash = "Q1W2E3R4T5Y6U7I8O9", CreatedDate = new DateTime(2023, 10, 31, 19, 25, 8) },
                new User { UserName = "grace_kelly", PasswordHash = "V5C4X3Z2A1S9D8F7G6", CreatedDate = new DateTime(2022, 12, 25, 12, 0, 0) },
                new User { UserName = "henry_ford", PasswordHash = "N8M7B6V5C4X3Z2L1K9", CreatedDate = new DateTime(2024, 4, 9, 7, 35, 42) },
                new User { UserName = "ivy_league", PasswordHash = "J6H5G4F3D2S1A0Z9X8", CreatedDate = new DateTime(2023, 6, 17, 21, 10, 30) }
            };
        }
    }
}

