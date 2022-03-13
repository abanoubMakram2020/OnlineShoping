using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class ProductOutputDTO:BaseOutputDTO<int>
    {
        public ProductOutputDTO()
        {
            CartItems = new List<CartItemOutputDTO>();
        }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptinEN { get; set; }
        public string DescriptinAr { get; set; }
        public string ImageURL { get; set; }
        public double AvalaibleQuentity { get; set; }
        public int CategoryId { get; set; }
        public CategoryOutputDTO Category { get; set; }
        public List<CartItemOutputDTO> CartItems { get; set; }
    }
}
