using LaptopStore.Business.DTOs;
using LaptopStore.Business.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Business.Services.Contracts
{
    public interface  IProductService : IBaseService<ProductDTO>
    {
        Task<IEnumerable<ProductDTO>> GetProductsByBrandAsync(int brandId);
        Task<IEnumerable<ProductDTO>> GetFeaturedProductsAsync();

    }
}
