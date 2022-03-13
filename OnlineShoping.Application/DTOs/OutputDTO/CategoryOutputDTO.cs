using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class CategoryOutputDTO : BaseOutputDTO<int>
    {
        public CategoryOutputDTO()
        {
            Products = new List<ProductOutputDTO>();
        }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public List<ProductOutputDTO> Products { get; set; }
    }
}
