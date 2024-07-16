using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Controllers
{
    // https://localhost:portaNumber/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET ALL CATEGORIES
        // GET: https://locahost:portNumber/api/categories
        [HttpGet]
        public IActionResult GetAll()
        {
            #region Hard coded List to populate the HttpGet method
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Utilities",
                    Abbr = "UTIL",
                    CategoyImageUrl = "https://images.pexels.com/photos/22690739/pexels-photo-22690739/free-photo-of-cityscape-of-auckland-viewed-from-the-one-tree-hill.jpeg"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Transportation",
                    Abbr = "TRANSP",
                    CategoyImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg"
                }
            };
            #endregion

            return Ok(categories);
        }
    }
}
