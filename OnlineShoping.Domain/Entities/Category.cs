using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoping.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Required]
        [MaxLength(50)]
        public string NameEn { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameAr { get; set; }

        public List<Product> Products { get; set; }



    }
}
