﻿using System.ComponentModel.DataAnnotations;

namespace ExerciseProject.Core.Models.Accounts
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}