using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceOne.WebApp
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories {get; set;}

        public Category Category {get; set;}
    }
}
