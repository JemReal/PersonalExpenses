using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

        Task<Category> CreateAsync(Category category);

        Task<Category?> UpdateAsync(Guid id, Category category);

        Task<Category?> DeteleAsync(Guid id);

    }
}
