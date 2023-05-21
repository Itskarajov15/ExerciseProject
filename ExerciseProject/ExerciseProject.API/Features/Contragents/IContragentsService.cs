namespace ExerciseProject.API.Features.Contragents
{
    public interface IContragentsService
    {
        Task<bool> Create(ContragentDto contragent);

        Task<IEnumerable<ContragentDto>> GetContragentsByUserId(int userId);
    }
}