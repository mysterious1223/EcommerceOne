using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcommerceOne.WebApp.Models;
using EcommerceOne.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceOne.WebApp.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var CatModel = await _db.Category.ToListAsync();    

            CategoryViewModel Categoryviewmodel = new CategoryViewModel () {
                Categories = CatModel,
                Category = new Category()
            };

            return View(Categoryviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategoryAsync(Category category)
        {
            // we will need a new view model. one to pass the list of current categories and one for the new category
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            await _db.Category.AddAsync(category);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // API Call to delete?
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index)); 
            }

            // delete

            var category = await _db.Category.FindAsync(id);
             _db.Category.Remove(category);

            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(Index)); 
        }
        [ActionName("EditCategory")]
        public async Task<IActionResult> EditCategory (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index)); 
            }

            // delete

            var category = await _db.Category.FindAsync(id);
            
            if(category == null)
            {
                return RedirectToAction(nameof(Index)); 
            }
            
            return Json (category);
        }

        [ActionName("EditCategoryPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategoryPost (Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            // delete

            var categoryFromDb = await _db.Category.FindAsync(category.Id);
            
            categoryFromDb.Name = category.Name;

            await _db.SaveChangesAsync();

            
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
