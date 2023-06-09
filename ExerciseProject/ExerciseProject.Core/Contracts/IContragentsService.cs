﻿using ExerciseProject.Core.Models.Contragents;

namespace ExerciseProject.Core.Contracts
{
    public interface IContragentsService
    {
        Task<bool> Create(ContragentViewModel contragent);

        Task<IEnumerable<ContragentViewModel>> GetAllByUserId(int userId);

        Task<EditContragentViewModel> GetContragentForEdit(int contragentId);

        Task EditContragent(EditContragentViewModel contragent);
    }
}