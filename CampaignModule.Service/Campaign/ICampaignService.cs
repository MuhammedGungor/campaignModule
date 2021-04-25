using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Campaign
{
    public interface ICampaignService
    {
        Task<string> CreateAsync(List<string> commands);

        Task<string> GetAsync(List<string> commands);
    }
}
