using CampaignModule.Domain.Campaign;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Campaign
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository<CampaignItem> _campaignRepository;

        public CampaignService(ICampaignRepository<CampaignItem> campaignRepository)
        {
            this._campaignRepository = campaignRepository;
        }

        public async Task<string> CreateAsync(List<string> commands)
        {
            var campaignItem = new CampaignItem(name: commands[1], code: commands[2], duration: commands[3], priceManipulationLimit: commands[4], targetSalesCount: commands[5], status: true);

            return await _campaignRepository.CreateAsync(campaignItem);
        }

        public async Task<string> GetAsync(List<string> commands)
        {
            var campaign = await _campaignRepository.GetAsync(commands[1]);

            return ResponseHelper.GetInstance().GetResponse(Constants.CampaignConstant.GetCampaignMessage,
                    new object[]
                    {
                        campaign.Name,
                        campaign.Status ? "Active" : "Ended",
                        campaign.TargetSalesCount,
                        campaign.TotalSales,
                        campaign.Turnover,
                        campaign.AverageItemPrice
                    });
        }
    }
}
