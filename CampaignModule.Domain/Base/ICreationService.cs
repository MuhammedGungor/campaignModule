using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Base
{
    public interface ICreationService<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method provide to create a record of TEntity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Creation message. For example; Product created P1 </returns>
        Task<string> CreateAsync(TEntity entity);
    }
}
