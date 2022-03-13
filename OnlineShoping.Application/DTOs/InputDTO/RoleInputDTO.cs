using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class RoleInputDTO:BaseInputDTO<int>
    {
        public RoleInputDTO()
        {
            Users = new List<UserInputDTO>();
        }

        public string Description { get; set; }
        public List<UserInputDTO> Users { get; set; }
    }
}
