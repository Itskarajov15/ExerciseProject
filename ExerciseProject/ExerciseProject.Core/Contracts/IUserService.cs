using ExerciseProject.Core.Models.Accounts;

namespace ExerciseProject.Core.Contracts
{
    public interface IUserService
    {
        Task<int> UserExists(LoginViewModel user);

        Task<bool> RegisterUser(RegisterViewModel user);
    }
}