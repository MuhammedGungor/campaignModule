using CampaignModule.Domain.Campaign;
using CampaignModule.Domain.Product;
using CampaignModule.Domain.Timer;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Timer
{
    public class TimerService : ITimerService
    {
        private readonly ITimerRepository<TimerItem> _timerRepository;
        private readonly ICampaignRepository<CampaignItem> _campaignRepository;
        private readonly IProductRepository<ProductItem> _productRepository;

        public TimerService(ITimerRepository<TimerItem> timerRepository,IProductRepository<ProductItem> productRepository, ICampaignRepository<CampaignItem> campaignRepository)
        {
            this._timerRepository = timerRepository;
            this._campaignRepository = campaignRepository;
            this._productRepository = productRepository;
        }

        public async Task<string> CreateAsync(List<string> commands)
        {
            if (commands.Count != 2)
                throw new Exception("Command parameters aren't as expected.");

            var hour = Convert.ToInt32(commands[1]);

            var timerEntity = new TimerItem() { Hour = hour };

            var response = await _timerRepository.CreateAsync(timerEntity);
            
            var campaignList = await _campaignRepository.GetAllAsync();

            if (campaignList != null && campaignList.Count > 0)
            {
                campaignList = campaignList.Where(c => c.Status).ToList();

                foreach (var campaign in campaignList)
                {
                    var remainingTime = campaign.RemainingDuration;

                    if (remainingTime >= hour)
                    {
                        var product = await _productRepository.GetAsync(campaign.ProductCode);

                        if (product != null)
                        {
                            var limitCalculatedValue = product.Price - (product.Price * campaign.PriceManipulationLimit / 100);

                            var rateOfIncrease = 0.5 * 1 / (double)campaign.RemainingDuration;

                            var decreaseValue = Convert.ToInt32(product.CampaignPrice * rateOfIncrease);

                            var priceFutureValue = product.CampaignPrice - decreaseValue;

                            if (limitCalculatedValue <= priceFutureValue)
                            {
                                //Update product with campaign price
                                product.CampaignPrice = priceFutureValue;
                                await _productRepository.UpdateAsync(product);

                                //Update remaining duration of campaign
                                campaign.RemainingDuration -= hour;
                                await _campaignRepository.UpdateAsync(campaign);
                            }
                            else
                            {
                                //Close the campaign
                                campaign.Status = false;
                                await _campaignRepository.UpdateAsync(campaign);

                                //Product campaign price should be normal price
                                product.CampaignPrice = product.Price;
                                await _productRepository.UpdateAsync(product);
                            }
                        }
                    }
                }
            }

            return response;
        }
    }
}
