using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Product
{
    public interface IProductRepository<TEntity> : ICreationService<TEntity>, IGetService where TEntity : class
    {

    }
}
