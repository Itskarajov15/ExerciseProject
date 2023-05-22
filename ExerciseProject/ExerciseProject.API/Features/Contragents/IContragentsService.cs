using ExerciseProject.API.Features.Contragents.Models;

namespace ExerciseProject.API.Features.Contragents
{
    public interface IContragentsService
    {
        Task<bool> Create(ContragentDto contragent);

        Task<IEnumerable<ContragentDto>> GetContragentsByUserId(int userId);

        Task EditContragent(EditContragentDto contragent);

        Task<EditContragentDto> GetContragentForEdit(int id);
    }
}