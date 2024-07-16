using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Data
{
    public class PersonalExpensesDbContext: DbContext
    {

        public PersonalExpensesDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Frequency> Frequencies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }
    }
}
