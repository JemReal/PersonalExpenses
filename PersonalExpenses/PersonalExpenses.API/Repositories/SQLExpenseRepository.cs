using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public class SQLExpenseRepository : IExpenseRepository
    {
        private readonly PersonalExpensesDbContext dbContext;

        public SQLExpenseRepository(PersonalExpensesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            await dbContext.Expenses.AddAsync(expense);
            await dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense?> DeleteSync(Guid id)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x  => x.Id == id);

            if (existingExpense == null)
            {
                return null;
            }

            dbContext.Expenses.Remove(existingExpense);
            await dbContext.SaveChangesAsync();

            return existingExpense;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await dbContext.Expenses.Include("Frequency").Include("Category").ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await dbContext.Expenses
                .Include("Frequency")
                .Include("Category")
                .FirstOrDefaultAsync(x  => x.Id == id);
        }

        public async Task<Expense?> UpdateAsync(Guid id, Expense expense)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (existingExpense == null)
            {
                return null;
            }

            existingExpense.Name = expense.Name;
            existingExpense.Description = expense.Description;
            existingExpense.Quantity = expense.Quantity;
            existingExpense.ExpenseImageUrl = expense.ExpenseImageUrl;
            existingExpense.FrequencyId = expense.FrequencyId;
            existingExpense.CategoryId = expense.CategoryId;

            await dbContext.SaveChangesAsync();
            return existingExpense;
        }
    }
}
