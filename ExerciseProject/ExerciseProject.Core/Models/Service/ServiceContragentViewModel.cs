namespace ExerciseProject.Core.Models.Service
{
    public class ServiceContragentViewModel
    {
        public bool IsValid { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string VAT { get; set; } = null!;
    }
}