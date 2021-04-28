using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Product
{
    public interface IProductRepository<TEntity> : ICreationService<TEntity>, IGetService<TEntity>, IListService<List<TEntity>> where TEntity : class
    {
        Task<bool> UpdateAsync(ProductItem product);
    }
}
