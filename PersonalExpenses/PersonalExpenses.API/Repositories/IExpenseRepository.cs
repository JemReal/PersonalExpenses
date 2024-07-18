using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public interface IExpenseRepository
    {
        Task<Expense> CreateAsync(Expense expense);

        Task<List<Expense>> GetAllAsync();

        Task<Expense?> GetByIdAsync(Guid id);

        Task<Expense?> UpdateAsync(Guid id, Expense expense);

        Task<Expense?> DeleteSync(Guid id);
    }
}
