using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class CartOutputDTO:BaseOutputDTO<int>
    {
        public CartOutputDTO()
        {
            CartItems = new List<CartItemOutputDTO>();
        }

        [Required]
        public decimal Total { get; set; }
        public List<CartItemOutputDTO> CartItems { get; set; }
        public int UserId { get; set; }

        public UserOutputDTO User { get; set; }
    }
}
