using System.ComponentModel.DataAnnotations;

namespace PersonalExpenses.API.Models.DTO
{
    public class AddCategoryRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Abbreviation has to be minimum of 3 characters.")]
        [MaxLength(6, ErrorMessage = "Abbreviation has to be maximum of 6 characters.")]
        public string Abbr { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Abbreviation has to be maximum of 100 characters.")]
        public string Name { get; set; }

        public string? CategoyImageUrl { get; set; }
    }
}
