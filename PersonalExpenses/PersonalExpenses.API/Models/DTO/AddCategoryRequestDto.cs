namespace PersonalExpenses.API.Models.DTO
{
    public class AddCategoryRequestDto
    {
        public string Abbr { get; set; }

        public string Name { get; set; }

        public string? CategoyImageUrl { get; set; }
    }
}
