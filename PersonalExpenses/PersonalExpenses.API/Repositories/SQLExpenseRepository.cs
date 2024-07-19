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

        public async Task<List<Expense>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var expenses = dbContext.Expenses.Include("Frequency").Include("Category").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = expenses.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = isAscending ? expenses.OrderBy(x => x.Name) : expenses.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = isAscending ? expenses.OrderBy(x => x.Quantity) : expenses.OrderByDescending(x => x.Quantity);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await expenses.Skip(skipResults).Take(pageSize).ToListAsync();

            // w/out filter, sorting, and pagination
            // return await dbContext.Expenses.Include("Frequency").Include("Category").ToListAsync();
        }

        public Task<List<Expense>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true)
        {
            throw new NotImplementedException();
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
