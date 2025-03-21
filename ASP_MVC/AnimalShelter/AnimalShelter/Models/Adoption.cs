namespace AnimalShelter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Adoption
    {
        [Key] // FK
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Adoption Date is required.")]
        [Display(Name = "Adoption Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AdoptionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "User ID is required.")]
        [Display(Name = "Adopted By")]
        public string UserId { get; set; } // FK

        public Animal Animal { get; set; } // One -> One

        public Adoption()
        {
            AdoptionDate = DateTime.Now;
        }
    }
}
