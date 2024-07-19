using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public interface IExpenseRepository
    {
        Task<Expense> CreateAsync(Expense expense);

        Task<List<Expense>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);

        Task<Expense?> GetByIdAsync(Guid id);

        Task<Expense?> UpdateAsync(Guid id, Expense expense);

        Task<Expense?> DeleteSync(Guid id);
    }
}
