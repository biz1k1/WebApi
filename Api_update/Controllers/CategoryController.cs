using CashbackApi.Data;
using CashbackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashbackApi.Controllers {
    [ApiController]
    [Route(template: "[controller]")]
    public class CategoryController : ControllerBase {
        private readonly DataContext _context;
        public CategoryController(DataContext context) {
            _context = context;
        }
        [HttpGet]
        [Route(template:"GetAll")]
        public async Task<IActionResult> Get() {
            return Ok(await _context.Category.ToListAsync());
        }
        [HttpGet]
        [Route(template: "GetById")]
        public async Task<IActionResult> GetID(int id) {
            var GetId=await _context.Category.FirstOrDefaultAsync(x=>x.CategoryId==id);
            if (GetID == null) return NotFound(); 
            return Ok(GetId);
        }

        [HttpPost]
        [Route(template:"AddCategory")]
        public async Task<IActionResult> AddCategory(int id,string category,double procent ){
            var ExistCard = await _context.CardInfo.FirstOrDefaultAsync(x => x.CardId == id);
            if (ExistCard == null) return NotFound();
            var NewCategory = new Categories {
                CategoryName = category,
                CategoryValue = procent,
            };
            ExistCard.Category.Add(NewCategory);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route(template: "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null) return NotFound();

            _context.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch]
        [Route(template: "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id,string category, double cashbackcategory) {
            var CategoryExist = await _context.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (CategoryExist == null) {
                return NotFound();
            }
            CategoryExist.CategoryName = category;
            CategoryExist.CategoryValue = cashbackcategory;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
