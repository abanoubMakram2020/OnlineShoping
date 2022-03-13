using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoping.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public Role()
        {
            Users = new List<User>();
        }


        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
