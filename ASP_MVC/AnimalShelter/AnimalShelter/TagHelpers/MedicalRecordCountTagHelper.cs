using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Data;
using AnimalShelter.Models;

namespace TagHelpers
{
    public class MedicalRecordCountTagHelper : TagHelper
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordCountTagHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AnimalId { get; set; }
        public string AnimalName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span"; // Name

            // Count records
            int recordCount = await _context.MedicalRecords
                                            .Where(r => r.AnimalId == AnimalId)
                                            .CountAsync();

            string content = recordCount > 0
                ? $"{AnimalName} has {recordCount} medical records."
                : $"{AnimalName} has no medical records.";

            output.Content.SetContent(content);
        }
    }
}
