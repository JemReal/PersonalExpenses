using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Data
{
    public class PersonalExpensesDbContext: DbContext
    {

        public PersonalExpensesDbContext(DbContextOptions<PersonalExpensesDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Frequency> Frequencies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Frequencies
            // Daily, Weekly, and Monthly
            var frequencies = new List<Frequency>()
            {
                new Frequency()
                {
                    Id = Guid.Parse("24413659-47c5-4fba-8ace-6ef37060b9e5"),
                    Name = "Daily"
                },
                new Frequency()
                {
                    Id = Guid.Parse("1b358da8-c0d7-4e6a-933c-a7568a687a5d"),
                    Name = "Weekly"
                },
                new Frequency()
                {
                    Id = Guid.Parse("341bb587-72ed-4231-85c7-82c436080fbc"),
                    Name = "Monthly"
                }
            };

            // Seed frequencies to the database.
            modelBuilder.Entity<Frequency>().HasData(frequencies);

            // Seed data for Categories
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("9aa916c3-97f4-4906-9214-6755ee0023ff"),
                    Abbr = "UTILS",
                    Name = "Utilities",
                    CategoyImageUrl = "https://images.pexels.com/photos/2898199/pexels-photo-2898199.jpeg"
                },
                new Category
                {
                    Id = Guid.Parse("d880b23f-8fab-4b1a-af6f-1a2603b266ed"),
                    Abbr = "TRANSP",
                    Name = "Transportation",
                    CategoyImageUrl = "https://images.pexels.com/photos/210182/pexels-photo-210182.jpeg"
                },
                new Category
                {
                    Id = Guid.Parse("c4dd6eb2-51c9-4174-86ba-3a56cbe9a05f"),
                    Abbr = "FOOD",
                    Name = "Food",
                    CategoyImageUrl = "https://www.food-safety.com/ext/resources/fsm/cache/file/26EC8DA4-CFD1-4437-897750814E836EBE.png"
                },
                new Category
                {
                    Id = Guid.Parse("1083f4c1-51a5-4506-934d-23145054fd7b"),
                    Abbr = "CLOTH",
                    Name = "Clothing",
                    CategoyImageUrl = "https://images.pexels.com/photos/3812433/pexels-photo-3812433.jpeg"
                }
            };

            // Seed categories to the database.
            modelBuilder.Entity<Category>().HasData(categories);

        }
    }
}
