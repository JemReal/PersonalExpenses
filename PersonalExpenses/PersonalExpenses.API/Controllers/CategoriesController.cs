using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenses.API.Data;
using PersonalExpenses.API.Models.Domain;
using PersonalExpenses.API.Models.DTO;

namespace PersonalExpenses.API.Controllers
{
    // https://localhost:portaNumber/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly PersonalExpensesDbContext dbContext;

        public CategoriesController(PersonalExpensesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL CATEGORIES
        // GET: https://locahost:portNumber/api/categories
        [HttpGet]
        public IActionResult GetAll()
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
            var categoriesDomain = dbContext.Categories.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var category = dbContext.Categories.Find(id);
            // Get Category Domain Model from Database.
            var categoryDomain = dbContext.Categories.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            // Map or convert DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Abbr = addCategoryRequestDto.Abbr,
                Name = addCategoryRequestDto.Name,
                CategoyImageUrl = addCategoryRequestDto.CategoyImageUrl
            };

            // Use Domain Model to create Category
            dbContext.Categories.Add(categoryDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            // Check if category exists
            var categoryDomainModel = dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            categoryDomainModel.Abbr = updateCategoryRequestDto.Abbr;
            categoryDomainModel.Name = updateCategoryRequestDto.Name;
            categoryDomainModel.CategoyImageUrl = updateCategoryRequestDto.CategoyImageUrl;

            dbContext.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Check first if the category provided from the route {id} does exists.
            var categoryDomainModel = dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (categoryDomainModel == null)
            {
                return NotFound(); ;
            }

            // Delete category
            dbContext.Categories.Remove(categoryDomainModel);
            dbContext.SaveChanges();

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
