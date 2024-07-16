namespace PersonalExpenses.API.Models.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Quantity { get; set; }

        public string? ExpenseImageUrl { get; set; }

        public Guid FrequencyId { get; set; }

        public Guid CategoryId { get; set; }


        // Navigation Properties
        public Frequency Frequency { get; set; }

        public Category Category { get; set; }


    }
}
