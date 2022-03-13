using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoping.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public Product()
        {
            CartItems = new List<CartItem>();
        }
        [Required]
        [MaxLength(50)]
        public string NameEn { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameAr { get; set; }

        [MaxLength(1024)]
        public string DescriptinEN { get; set; }
        [MaxLength(1024)]
        public string DescriptinAr { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImageURL { get; set; }
        [Required]
        public double AvalaibleQuentity { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<CartItem> CartItems { get; set; }


    }
}
