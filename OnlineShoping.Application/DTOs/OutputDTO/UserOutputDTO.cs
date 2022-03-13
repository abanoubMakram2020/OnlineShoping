using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class UserOutputDTO : BaseOutputDTO<int>
    {
        public UserOutputDTO()
        {
            Carts = new List<CartOutputDTO>();
        }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public RoleOutputDTO Role { get; set; }
        public List<CartOutputDTO> Carts { get; set; }
    }
}
