using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoping.Domain.Entities
{
    public class Cart : BaseEntity<int>
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
     
        [Required]
        public decimal Total { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
