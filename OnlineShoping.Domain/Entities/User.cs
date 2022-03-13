using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoping.Domain.Entities
{
    public class User //: BaseEntity<int>
    {
        public User()
        {
            Carts = new List<Cart>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(18)]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public List<Cart> Carts { get; set; }

    }
}
