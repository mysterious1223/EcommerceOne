using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceOne.WebApp
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Item Name")]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }


        // URL? or the image binary?
        public byte[] ImageIcon { get; set; }


        // do we need category? We can just retrieve it from referencing the category table
        // maybe only allow the Subcategory to be selected
        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
 

    }
}
