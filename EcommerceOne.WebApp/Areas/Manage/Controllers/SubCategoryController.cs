using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EcommerceOne.WebApp.Models;
using EcommerceOne.WebApp.Data;
using Microsoft.EntityFrameworkCore;


namespace EcommerceOne.WebApp.Controllers
{
    [Area("Manage")]
    public class SubCategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var SubViewModel = new SubCategoryViewModel () {
                SubCategory = new SubCategory(),
                Categories = await _db.Category.ToListAsync(),
                SubCategories = await _db.SubCategory.ToListAsync()

            };

            return View(SubViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubCategoryAsync(SubCategory subcategory)
        {
            // we will need a new view model. one to pass the list of current categories and one for the new category
            if(!ModelState.IsValid)
            {
                return View(subcategory);
            }

            await _db.SubCategory.AddAsync(subcategory);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // API Call to delete?
        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategory (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index)); 
            }

            // delete

            var Subcategory = await _db.SubCategory.FindAsync(id);
             _db.SubCategory.Remove(Subcategory);

            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(Index)); 
        }
        [ActionName("EditSubCategory")]
        public async Task<IActionResult> EditSubCategory (int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index)); 
            }

            // delete

            var SubCategoryFromDb = await _db.SubCategory.FindAsync(id);
            var CategoryFromDb = await _db.Category.FindAsync(SubCategoryFromDb.CategoryId);
            if(SubCategoryFromDb == null )
            {
                return RedirectToAction(nameof(Index)); 
            }
            
            var returned = new {
                name = SubCategoryFromDb.Name,
                id = SubCategoryFromDb.Id,
                catname = CategoryFromDb.Name

            };




            return Json (returned);
        }
        
        [ActionName("EditSubCategory")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubCategory (SubCategory Subcategory)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            // delete

            var subcategoryFromDb = await _db.SubCategory.FindAsync(Subcategory.Id);
            
            subcategoryFromDb.Name = Subcategory.Name;

            await _db.SaveChangesAsync();

            
            return RedirectToAction(nameof(Index));
        }

        [ActionName("GetSubcategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            //subCategories = await (from subCategory in _db.SubCategory
              //               where subCategory.CategoryId == id
                //             select subCategory).ToListAsync();
                
            subCategories = await _db.SubCategory.Where(s => s.CategoryId == id).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Name"));
        }


    }
}
