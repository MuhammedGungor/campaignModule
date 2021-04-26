using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Timer
{
    public interface ITimerService 
    {
        Task<string> CreateAsync(List<string> commands);
    }
}
