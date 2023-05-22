namespace ExerciseProject.API.Features.Contragents.Models
{
    public class EditContragentDto
    {
        public int Id { get; set; }

        public string Address { get; set; } = null!;

        public string? Mail { get; set; }

        public string VAT { get; set; } = null!;

        public int UserId { get; set; }
    }
}