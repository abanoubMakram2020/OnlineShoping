using System.Collections.Generic;

namespace OnlineShoping.Application.DTOs.InputDTO
{
    public class CategoryInputDTO:BaseInputDTO<int>
    {
        public CategoryInputDTO()
        {
            Products = new List<ProductInputDTO>();
        }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public List<ProductInputDTO> Products { get; set; }
    }
}
