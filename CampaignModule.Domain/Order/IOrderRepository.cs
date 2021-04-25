using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Order
{
    public interface IOrderRepository<TEntity> : ICreationService<TEntity> where TEntity : class
    {

    }
}
