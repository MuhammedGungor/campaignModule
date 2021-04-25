using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Campaign
{
    public interface ICampaignRepository<TEntity> : ICreationService<TEntity>, IGetService where TEntity : class
    {

    }
}
