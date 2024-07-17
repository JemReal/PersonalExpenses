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

        public CategoriesController(PersonalExpensesDbContext dbContext, ICategoryRepository categoryRepository)
        {
            this.dbContext = dbContext;
            this.categoryRepository = categoryRepository;
        }

        // GET ALL CATEGORIES
        // GET: https://locahost:portNumber/api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            #region Hard coded List to populate the HttpGet method
            //var categories = new List<Category>
            //{
            //    new Category
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Utilities",
            //        Abbr = "UTIL",
            //        CategoyImageUrl = "https://images.pexels.com/photos/22690739/pexels-photo-22690739/free-photo-of-cityscape-of-auckland-viewed-from-the-one-tree-hill.jpeg"
            //    },
            //    new Category
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Transportation",
            //        Abbr = "TRANSP",
            //        CategoyImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg"
            //    }
            //};
            #endregion
            // Get data from database - Domain Models
            var categoriesDomain = await categoryRepository.GetAllAsync();

            // Map Domain Models to DTO before sending back to the client
            // Map this Domain Models to Data Object Transfer (DTO)
            var categoriesDto = new List<CategoryDto>();
            foreach (var categoryDomain in categoriesDomain) 
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = categoryDomain.Id,
                    Abbr = categoryDomain.Abbr,
                    Name = categoryDomain.Name,
                    CategoyImageUrl = categoryDomain.CategoyImageUrl

                });
            }
            
            // Return DTO

            return Ok(categoriesDto);
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

            // Map or convert the Category Domain Model to Category DTO.
            var categoryDto = new CategoryDto
            {
                Id = categoryDomain.Id,
                Abbr = categoryDomain.Abbr,
                Name = categoryDomain.Name,
                CategoyImageUrl = categoryDomain.CategoyImageUrl
            };

            // Return DTO back to client. XXX
            return Ok(categoryDto);
        }

        // POST to create new Category
        // POST: https://localhost:portnumber/api/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            // Map or convert DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Abbr = addCategoryRequestDto.Abbr,
                Name = addCategoryRequestDto.Name,
                CategoyImageUrl = addCategoryRequestDto.CategoyImageUrl
            };

            // Use Domain Model to create Category
            categoryDomainModel = await categoryRepository.CreateAsync(categoryDomainModel);

            // Map or convert Domain Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Abbr = categoryDomainModel.Abbr,
                Name = categoryDomainModel.Name,
                CategoyImageUrl = categoryDomainModel.CategoyImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);

        }

        // UPDATE Category
        // PUT: https://localhost:portnumber/api/categies/{id}
        [HttpPut]
        [Route("{id=Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Abbr = updateCategoryRequestDto.Abbr,
                Name = updateCategoryRequestDto.Name,
                CategoyImageUrl = updateCategoryRequestDto.CategoyImageUrl
            };

            // Check if category exists
            categoryDomainModel = await categoryRepository.UpdateAsync(id, categoryDomainModel);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Abbr = categoryDomainModel.Abbr,
                Name = categoryDomainModel.Name,
                CategoyImageUrl = categoryDomainModel.CategoyImageUrl
            };

            return Ok(categoryDto);

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

            // Return the deleted category back.
            // Map Domain Model to DTO
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Abbr = categoryDomainModel.Abbr,
                Name = categoryDomainModel.Name,
                CategoyImageUrl = categoryDomainModel.CategoyImageUrl
            };

            return Ok(categoryDto);
        }
    }
}
