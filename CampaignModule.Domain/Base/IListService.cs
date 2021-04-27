using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Base
{
    public interface IListService<TEntity> where TEntity : class
    {
        Task<TEntity> GetAllAsync();
    }
}
