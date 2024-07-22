using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalExpenses.API.CustomActionFilters;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Models.Domain;
using PersonalExpenses.API.Models.DTO;
using PersonalExpenses.API.Repositories;
using System.Text.Json;

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
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(PersonalExpensesDbContext dbContext, 
            ICategoryRepository categoryRepository, 
            IMapper mapper,
            ILogger<CategoriesController> logger)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL CATEGORIES
        // GET: https://locahost:portNumber/api/categories
        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception.");

                // Get data from database - Domain Models
                var categoriesDomain = await categoryRepository.GetAllAsync();

                // Return DTO

                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(categoriesDomain)}");

                return Ok(mapper.Map<List<CategoryDto>>(categoriesDomain));

            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                throw;
            }            
        }

        // GET SINGLE CATEGORY (Get Category By ID)
        // GET: https://localhost:portnumber/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            
            // Map or convert DTO to Domain Model
            var categoryDomainModel = mapper.Map<Category>(addCategoryRequestDto);

            // Use Domain Model to create Category
            categoryDomainModel = await categoryRepository.CreateAsync(categoryDomainModel);

            // Map or convert Domain Model to DTO
            var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
         
        }

        // UPDATE Category
        // PUT: https://localhost:portnumber/api/categies/{id}
        [HttpPut]
        [Route("{id=Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
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

        // DELETE a Category
        // DELETE: https://localhost:portnumber/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
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
