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

    }
}
