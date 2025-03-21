namespace AnimalShelter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MedicalRecord
    {
        public int Id { get; set; } // PK

        [Required(ErrorMessage = "Animal ID is required.")]
        [Display(Name = "Animal")]
        public int AnimalId { get; set; } // FK

        [Required(ErrorMessage = "Record Date is required.")]
        [Display(Name = "Record Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecordDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public Animal? Animal { get; set; } // One -> Many
    }
}
