using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class CartInputDTO:BaseInputDTO<int>
    {
        public CartInputDTO()
        {
            CartItems = new List<CartItemInputDTO>();
        }

        public decimal Total { get; set; }
        public List<CartItemInputDTO> CartItems { get; set; }
        public int UserId { get; set; }
        public UserInputDTO User { get; set; }
    }
}
