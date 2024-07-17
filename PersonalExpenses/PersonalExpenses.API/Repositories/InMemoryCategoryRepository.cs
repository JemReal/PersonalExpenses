using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        public async Task<List<Category>> GetAllAsync()
        {
            return new List<Category>
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Abbr = "UTILS",
                    Name = "Utilities",
                    CategoyImageUrl = "some-image.jpg"
                }
            };
        }
    }
}
