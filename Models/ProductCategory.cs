using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductCategoryName { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new Collection<Product>();
        }
    }
}
