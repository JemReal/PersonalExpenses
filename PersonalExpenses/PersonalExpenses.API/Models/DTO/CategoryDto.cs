namespace PersonalExpenses.API.Models.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Abbr { get; set; }

        public string Name { get; set; }

        public string? CategoyImageUrl { get; set; }
    }
}
