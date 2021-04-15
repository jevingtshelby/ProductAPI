using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers.Resources
{
    public class ProductResource
    {

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }
        public string Expiry { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string LastUpdatedBy { get; set; }
        //i added below
        public int ProductCategoryId { get; set; }

    }
}
