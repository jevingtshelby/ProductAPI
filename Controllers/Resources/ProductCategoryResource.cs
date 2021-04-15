using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers.Resources
{
    public class ProductCategoryResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductCategoryName { get; set; }

        public ICollection<ProductResource> Products { get; set; }

        public ProductCategoryResource()
        {
            Products = new Collection<ProductResource>();
        }

    }
}
