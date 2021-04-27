using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public interface IListRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAllAsync();
    }
}
