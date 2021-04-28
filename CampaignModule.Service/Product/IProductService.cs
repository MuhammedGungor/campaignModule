using CampaignModule.Domain.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Product
{
    public interface IProductService
    {
        Task<string> CreateAsync(List<string> commands);

        Task<string> GetAsync(List<string> commands);

        Task<List<ProductItem>> GetAll();
    }
}
