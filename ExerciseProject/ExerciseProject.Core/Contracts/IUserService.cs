using ExerciseProject.Core.Models.Accounts;

namespace ExerciseProject.Core.Contracts
{
    public interface IUserService
    {
        Task<bool> UserExists(LoginViewModel user);

        Task<bool> RegisterUser(RegisterViewModel user);
    }
}