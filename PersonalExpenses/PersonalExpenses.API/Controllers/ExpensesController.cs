using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenses.API.CustomActionFilters;
using PersonalExpenses.API.Models.Domain;
using PersonalExpenses.API.Models.DTO;
using PersonalExpenses.API.Repositories;

namespace PersonalExpenses.API.Controllers
{
    // api/expenses
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IExpenseRepository expenseRepository;

        public ExpensesController(IMapper mapper, IExpenseRepository expenseRepository)
        {
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
        }

        // CREATE Expense
        // POST: https://locahost:portnumber/api/expenses
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddExpenseRequestDto addExpenseRequestDto)
        {
            
            // Map DTO to Domain Model (from AddExpenseRequestDto to Expense Domain Model)
            var expenseDomainModel = mapper.Map<Expense>(addExpenseRequestDto);

            await expenseRepository.CreateAsync(expenseDomainModel);

            // Map Domain Model to DTO

            return Ok(mapper.Map<ExpenseDto>(expenseDomainModel));            
        }

        // GET Expenses
        // GET: https://localhost:portnumber/api/expenses - return w/out filtering, sorting, and pagination
        // GET: https://localhost:portnumber/api/expenses?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var expensesDomainModel = await expenseRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<ExpenseDto>>(expensesDomainModel));
        }

        // GET Expense By Id
        // GET: https://localhost:portnumber/api/expenses/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var expenseDomainModel = await expenseRepository.GetByIdAsync(id);

            if (expenseDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<ExpenseDto>(expenseDomainModel));
        }

        // UPDATE Expense by id
        // PUT: https://localhost:portnumber/api/expenses/{id]
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateExpenseRequestDto updateExpenseRequestDto)
        {            
            // Map DTO to Domain Model
            var expenseDomainModel = mapper.Map<Expense>(updateExpenseRequestDto);

            expenseDomainModel = await expenseRepository.UpdateAsync(id, expenseDomainModel);

            if (expenseDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<ExpenseDto>(expenseDomainModel));            
        }

        // DELETE an Expense by Id
        // DELETE: https://localhost:portnumber/api/expenses/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedExpenseDomainModel = await expenseRepository.DeleteSync(id);

            if (deletedExpenseDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<ExpenseDto>(deletedExpenseDomainModel));
        }
    }
}
