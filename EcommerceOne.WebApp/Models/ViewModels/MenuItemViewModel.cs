using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceOne.WebApp
{
    public class MenuItemViewModel
    {
        public IEnumerable<Item> items {get; set;}


        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<SubCategory> SubCategory { get; set; }
        public Item item {get; set;}


        

    }
}
