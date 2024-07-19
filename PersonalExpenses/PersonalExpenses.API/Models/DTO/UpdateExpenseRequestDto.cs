using System.ComponentModel.DataAnnotations;

namespace PersonalExpenses.API.Models.DTO
{
    public class UpdateExpenseRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public double Quantity { get; set; }

        public string? ExpenseImageUrl { get; set; }

        [Required]
        public Guid FrequencyId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
