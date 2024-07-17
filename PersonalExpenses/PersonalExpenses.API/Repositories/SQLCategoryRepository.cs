using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly PersonalExpensesDbContext dbContext;

        public SQLCategoryRepository(PersonalExpensesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeteleAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;

        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x  => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x =>x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Abbr = category.Abbr;
            existingCategory.Name = category.Name;
            existingCategory.CategoyImageUrl = category.CategoyImageUrl;

            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
