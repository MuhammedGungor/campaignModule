using CampaignModule.Domain.Campaign;
using CampaignModule.Domain.Order;
using CampaignModule.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public class CampaignRepository : BaseOperationService, ICampaignRepository<CampaignItem>
    {
        public async Task<string> CreateAsync(CampaignItem entity)
        {
            var campaignList = await base.GetValuesFromFolder<List<CampaignItem>>(Constants.CampaignConstant.StorePath);

            if (campaignList != null && campaignList.Exists(c => c.Name.Equals(entity.Name)))
                throw new Exception(Constants.CampaignConstant.SameCampaignExistMessage);

            if (campaignList == null)
                campaignList = new List<CampaignItem>();

            campaignList.Add(entity);

            var jsonable = JsonConvert.SerializeObject(campaignList);

            var createResponse = await base.WriteJson(Constants.CampaignConstant.StorePath, jsonable);

            if (createResponse)
                return ResponseHelper.GetInstance().GetResponse(Constants.CampaignConstant.CreateProductMessage, new object[] { entity.Name, entity.ProductCode, entity.Duration,entity.PriceManipulationLimit,entity.TargetSalesCount });
            else
                throw new Exception(Constants.General.CreateErrorMessage);
        }

        public async Task<List<CampaignItem>> GetAll()
        {
            var campaignList = await base.GetValuesFromFolder<List<CampaignItem>>(Constants.CampaignConstant.StorePath);

            return campaignList;
        }

        public async Task<CampaignItem> GetAsync(string id)
        {
            var campaignList = await base.GetValuesFromFolder<List<CampaignItem>>(Constants.CampaignConstant.StorePath);

            if (campaignList != null && campaignList.Count == 0)
                throw new Exception(Constants.ProductConstant.ProductsEmpty);

            var campaign = campaignList.FirstOrDefault(c => c.Name.Equals(id));

            if (campaign == null)
                throw new Exception(Constants.CampaignConstant.CampaignNotFound);

            //Bu kampanya ile bir sipariş verilmiş mi?
            var orderList = await base.GetValuesFromFolder<List<OrderItem>>(Constants.OrderConstant.StorePath);

            if (orderList != null && orderList.Count > 0)
            {
                var orders = orderList.Where(c => c.CampaignCode.Equals(id));

                if (orders.Count() > 0)
                {
                    var totalOrder = orders.Sum(c => c.Quantity);
                    campaign.TotalSales = totalOrder;

                    var totalPrice = orders.Sum(c => c.Quantity * c.Price);
                    campaign.AverageItemPrice = totalPrice / totalOrder;
                }
            }

            return campaign;
        }

        public async Task<bool> UpdateAsync(CampaignItem campaign)
        {
            var campaignList = await base.GetValuesFromFolder<List<CampaignItem>>(Constants.CampaignConstant.StorePath);

            if (campaignList == null && campaignList.Count == 0)
                throw new Exception(Constants.General.CreateErrorMessage);

            var oldCampaign = campaignList.FirstOrDefault(c => c.Name.Equals(campaign.Name));

            if (oldCampaign == null)
                throw new Exception(Constants.General.CreateErrorMessage);

            campaignList.Remove(oldCampaign);

            campaignList.Add(campaign);

            var jsonable = JsonConvert.SerializeObject(campaignList);

            var updateResponse = await base.WriteJson(Constants.CampaignConstant.StorePath, jsonable);

            return updateResponse;
        }
    }
}
