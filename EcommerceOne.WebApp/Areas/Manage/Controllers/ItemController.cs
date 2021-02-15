using System;
using System.IO;
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
    public class ItemController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            // WE ARE HERE. CHECKING IF THIS ALLOWS THE DROP DOWNS TO WORK
            // THEN ADD SUB CATEGORY DROP DOWN
            // ERROR not the correct view being returned? Double check the tutorial code if nothing we might need to create a new model
            MenuItemViewModel menuItems = new MenuItemViewModel () {
                items = await _db.Item.ToListAsync(),
                Category = await _db.Category.ToListAsync(),
                SubCategory = await _db.SubCategory.ToListAsync(),
                item = new Item()

            };
            
            return View(menuItems);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItemPostAsync(Item item)
        {   
            /*
                To display image
                <div class="col-12 border">
                @{
                    var base64 = Convert.ToBase64String(Model.Picture);
                    var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                }
                <img src="@imgsrc" height="100%" width="100%" />
        </div>
            */


            //price
            //description of item
            //image icon
            // we will need a new view model. one to pass the list of current categories and one for the new category
            if(!ModelState.IsValid)
            {

                return View(item);
            }
            
            var files = HttpContext.Request.Form.Files;
            if(files.Count>0)
            {
                byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    item.ImageIcon = p1;
            }

            await _db.Item.AddAsync(item);
            await _db.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        /*
        What will we need for the view?
            1. We need a list of sub categories
            2. Populate the Category field with the category of sub cat
            3. Item fields -> name, etc.
            4. Ability to list all items

            View Model -> MenuItemViewModel
        */
        

    }
}
