using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class UserInputDTO 
    {
        public UserInputDTO()
        {
            Carts = new List<CartInputDTO>();
        }
        public string FullName { get; set; }

      
        public string UserName { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public RoleInputDTO Role { get; set; }

        public List<CartInputDTO> Carts { get; set; }
    }
}
