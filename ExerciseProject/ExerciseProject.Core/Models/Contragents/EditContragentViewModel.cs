using System.ComponentModel.DataAnnotations;

namespace ExerciseProject.Core.Models.Contragents
{
    public class EditContragentViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        [Display(Name = "Address")]
        public string Address { get; set; } = null!;

        [StringLength(255, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        [Display(Name = "Mail")]
        [EmailAddress]
        public string? Mail { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "{0} must be {1} characters long.")]
        [Display(Name = "VAT")]
        public string VAT { get; set; } = null!;

        public int UserId { get; set; }
    }
}