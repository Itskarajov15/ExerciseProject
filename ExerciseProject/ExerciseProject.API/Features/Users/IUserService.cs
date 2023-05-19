using ExerciseProject.API.Features.Users.Models;

namespace ExerciseProject.API.Features.Users
{
    public interface IUserService
    {
        Task<bool> Create(UserDto userModel);

        Task<bool> UserExists(string name);

        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserById(int id);

        Task UpdateUser(int id, UserDto userModel);
    }
}