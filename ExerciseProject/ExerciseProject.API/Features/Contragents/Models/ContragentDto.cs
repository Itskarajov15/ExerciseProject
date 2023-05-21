namespace ExerciseProject.API.Features.Contragents
{
    public class ContragentDto
    {
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Mail { get; set; } = null!;

        public string VAT { get; set; } = null!;

        public int UserId { get; set; }
    }
}