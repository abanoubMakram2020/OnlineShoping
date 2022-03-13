using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class ProductInputDTO:BaseInputDTO<int>
    {
        public ProductInputDTO()
        {
            CartItems = new List<CartItemInputDTO>();
        }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptinEN { get; set; }
        public string DescriptinAr { get; set; }
        public string ImageURL { get; set; }
        public double AvalaibleQuentity { get; set; }
        public int CategoryId { get; set; }
        public CategoryInputDTO Category { get; set; }
        public List<CartItemInputDTO> CartItems { get; set; }
    }
}
