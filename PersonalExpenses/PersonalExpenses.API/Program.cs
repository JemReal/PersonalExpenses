using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonalExpensesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PersonalExpensesConnectionString")));
builder.Services.AddScoped<ICategoryRepository, SQLCategoryRepository>();
// builder.Services.AddScoped<ICategoryRepository, InMemoryCategoryRepository>(); // Smaple of changing the concrete implementation of DbContext. E.g., In Memory datasource.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
