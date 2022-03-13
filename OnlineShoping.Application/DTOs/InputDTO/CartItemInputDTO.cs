namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class CartItemInputDTO : BaseInputDTO<int>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public double Quentity { get; set; }
        public decimal UnitPrice { get; set; }
        public CartInputDTO Cart { get; set; }
        public ProductInputDTO Product { get; set; }
    }
}
