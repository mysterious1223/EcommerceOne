using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceOne.WebApp
{
    public class SubCategoryViewModel
    {
        public SubCategory SubCategory {get; set;}

        public IEnumerable<Category> Categories {get; set;}

        public List<SubCategory> SubCategories {get; set;}

        public string StatusMessage { get; set; }
    }
}
