using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Campaign
{
    public interface ICampaignRepository<TEntity> : ICreationService<TEntity>
                                                    , IGetService<TEntity>
                                                    , IListService<List<TEntity>> where TEntity : class
    {
        public Task<bool> UpdateAsync(CampaignItem campaign);
    }
}
