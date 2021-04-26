using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Domain.Base
{
    public interface IGetService<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method provide a response that includes detail of an entity. Get entity with id parameter.
        /// </summary>
        /// <typeparam name="TResponse">Generic response</typeparam>
        /// <param name="id">Should be a code or name</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string id);
    }
}
