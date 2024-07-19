using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Models.Domain;
using PersonalExpenses.API.Models.DTO;
using PersonalExpenses.API.Repositories;

namespace PersonalExpenses.API.Controllers
{
    // https://localhost:portaNumber/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly PersonalExpensesDbContext dbContext;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesController(PersonalExpensesDbContext dbContext, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // GET ALL CATEGORIES
        // GET: https://locahost:portNumber/api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Domain Models
            var categoriesDomain = await categoryRepository.GetAllAsync();

            // Return DTO
            return Ok(mapper.Map<List<CategoryDto>>(categoriesDomain));
        }

        // GET SINGLE CATEGORY (Get Category By ID)
        // GET: https://localhost:portnumber/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var category = dbContext.Categories.Find(id);
            // Get Category Domain Model from Database.
            var categoryDomain = await categoryRepository.GetByIdAsync(id);

            if (categoryDomain == null)
            {
                return NotFound();
            }

            // Return DTO back to client.
            return Ok(mapper.Map<CategoryDto>(categoryDomain));
        }

        // POST to create new Category
        // POST: https://localhost:portnumber/api/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            if(ModelState.IsValid)
            {
                // Map or convert DTO to Domain Model
                var categoryDomainModel = mapper.Map<Category>(addCategoryRequestDto);

                // Use Domain Model to create Category
                categoryDomainModel = await categoryRepository.CreateAsync(categoryDomainModel);

                // Map or convert Domain Model to DTO
                var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // UPDATE Category
        // PUT: https://localhost:portnumber/api/categies/{id}
        [HttpPut]
        [Route("{id=Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var categoryDomainModel = mapper.Map<Category>(updateCategoryRequestDto);

                // Check if category exists
                categoryDomainModel = await categoryRepository.UpdateAsync(id, categoryDomainModel);

                if (categoryDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE a Category
        // DELETE: https://localhost:portnumber/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoryDomainModel = await categoryRepository.DeteleAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
        }
    }
}
