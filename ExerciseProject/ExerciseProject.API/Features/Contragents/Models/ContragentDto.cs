namespace ExerciseProject.API.Features.Contragents
{
    public class ContragentDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Mail { get; set; }

        public string VAT { get; set; } = null!;

        public int UserId { get; set; }
    }
}