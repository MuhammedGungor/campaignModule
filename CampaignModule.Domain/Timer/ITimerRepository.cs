using CampaignModule.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Timer
{
    public interface ITimerRepository<TEntity> : ICreationService<TEntity> where TEntity : class
    {

    }
}
