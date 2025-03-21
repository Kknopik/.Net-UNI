namespace AnimalShelter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Animal name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [LettersOnlyValidator(ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Species is required.")]
        [StringLength(30, ErrorMessage = "Species cannot exceed 30 characters.")]
        [LettersOnlyValidator(ErrorMessage = "Species can only contain letters.")]
        public string Species { get; set; }

        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Adoption Status")]
        public StatusEnum Status { get; set; }

        public Adoption? Adoption { get; set; } // One -> One

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>(); //One -> Many
    }

    public enum StatusEnum
    {
        Available,
        Reserved,
        Adopted
    }
}
