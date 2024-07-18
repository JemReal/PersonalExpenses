namespace PersonalExpenses.API.Models.DTO
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Quantity { get; set; }

        public string? ExpenseImageUrl { get; set; }

        //public Guid FrequencyId { get; set; }

        //public Guid CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public FrequencyDto Frequency { get; set; }
    }
}
