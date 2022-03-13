using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class RoleOutputDTO : BaseOutputDTO<int>
    {
        public RoleOutputDTO()
        {
            Users = new List<UserOutputDTO>();
        }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public List<UserOutputDTO> Users { get; set; }
    }
}
