using System.ComponentModel.DataAnnotations;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class CartItemOutputDTO : BaseOutputDTO<int>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public double Quentity { get; set; }
        public decimal UnitPrice { get; set; }
        public CartOutputDTO Cart { get; set; }

        public ProductOutputDTO Product { get; set; }
    }
}
