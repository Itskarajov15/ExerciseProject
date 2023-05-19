using ExerciseProject.API.Features.Users.Models;
using Microsoft.Data.SqlClient;

namespace ExerciseProject.API.Features.Users
{
    public class UserService : IUserService
    {
        private readonly string connectionString;

        public UserService(IConfiguration config)
        {
            this.connectionString = config["ConnectionString"];
        }

        public async Task<bool> Create(UserDto userModel)
        {
            if (!await UserExists(userModel.Name))
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand(@"INSERT INTO Users (Name, Password)
                                                   VALUES
                                                   (@name, @password)", connection);

                    command.Parameters.AddWithValue("name", userModel.Name);
                    command.Parameters.AddWithValue("password", userModel.Password); //Hash the password

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }

            return false;
        }

        public async Task<User> GetUserById(int id)
        {
            User user = null;

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users WHERE Id = @id", connection);
                command.Parameters.AddWithValue("id", id);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Password = (string)reader["Password"]
                    };
                }
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users", connection);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new User { Id = (int)reader["Id"], Name = (string)reader["Name"], Password = (string)reader["Password"] });
                }
            }

            return users;
        }

        public async Task UpdateUser(int id, UserDto userModel)
        {
            if (await GetUserById(id) != null)
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand("UPDATE Users SET Name = @name, Password = @password WHERE Id = @id", connection);

                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("name", userModel.Name);
                    command.Parameters.AddWithValue("password", userModel.Password);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> UserExists(string name)
        {
            int result = 0;

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Name = @name", connection);
                command.Parameters.AddWithValue("name", name);

                await connection.OpenAsync();
                result = (int)await command.ExecuteScalarAsync();
            }

            return result > 0;
        }
    }
}