using CampaignModule.Domain.Campaign;
using CampaignModule.Domain.Order;
using CampaignModule.Domain.Product;
using CampaignModule.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public class OrderRepository : BaseOperationService, IOrderRepository<OrderItem>
    {
        public async Task<string> CreateAsync(OrderItem orderEntity)
        {
            var orderList = await base.GetValuesFromFolder<List<OrderItem>>(Constants.OrderConstant.StorePath);

            if (orderList == null)
                orderList = new List<OrderItem>();

            var productList = await base.GetValuesFromFolder<List<ProductItem>>(Constants.ProductConstant.StorePath);

            var product = productList.First(c => c.ProductCode.Equals(orderEntity.ProductCode));

            var campaignList = await base.GetValuesFromFolder<List<CampaignItem>>(Constants.CampaignConstant.StorePath);
            
            var baseQuantity = orderEntity.Quantity;

            if (campaignList != null && campaignList.Count > 0 && campaignList.FirstOrDefault(c => c.ProductCode.Equals(orderEntity.ProductCode) && c.Status && c.TargetSalesCount > 0) != null)
            {
                //First, get the campaign.
                var currentCampaign = campaignList.FirstOrDefault(c => c.ProductCode.Equals(orderEntity.ProductCode) && c.Status && c.TargetSalesCount > 0);

                //It will be decrease both from the campaign stock and from the product stock.
                if (currentCampaign != null && currentCampaign.TargetSalesCount <= orderEntity.Quantity)
                {
                    #region Order With Campaign

                    orderEntity.CampaignCode = currentCampaign.Name;
                    orderEntity.Price = product.CampaignPrice;
                    orderEntity.Quantity = currentCampaign.TargetSalesCount; //There are already more orders than the campaign stock. can take them all.

                    //save order
                    orderList.Add(orderEntity);

                    var campaignOrderJson = JsonConvert.SerializeObject(orderList);

                    await base.WriteJson(Constants.OrderConstant.StorePath, campaignOrderJson);

                    #endregion

                    #region Update Campaign

                    campaignList.Remove(currentCampaign);
                    currentCampaign.Status = false;
                    currentCampaign.TargetSalesCount = 0;

                    campaignList.Add(currentCampaign);

                    var campaignListJson = JsonConvert.SerializeObject(campaignList);
                    await base.WriteJson(Constants.CampaignConstant.StorePath, campaignListJson);

                    #endregion

                    #region Order Without Campaign if Needed

                    var quantityControl = baseQuantity - orderEntity.Quantity;

                    if (quantityControl > 0)
                    {
                        orderEntity.Quantity = quantityControl;
                        orderEntity.CampaignCode = string.Empty;
                        orderEntity.Price = product.Price;

                        orderList.Add(orderEntity);
                        var normalOrderJson = JsonConvert.SerializeObject(orderList);
                        await base.WriteJson(Constants.OrderConstant.StorePath, normalOrderJson);
                    }

                    #endregion

                    #region Update Product Stock and Price

                    productList.Remove(product);

                    product.CampaignPrice = product.Price;
                    product.Stock -= baseQuantity;

                    productList.Add(product);

                    var productJson = JsonConvert.SerializeObject(productList);

                    await base.WriteJson(Constants.OrderConstant.StorePath, productJson);

                    #endregion
                }
                else if (currentCampaign != null && currentCampaign.TargetSalesCount > orderEntity.Quantity)
                {
                    #region Order With Campaign

                    orderEntity.CampaignCode = currentCampaign.Name;
                    orderEntity.Price = product.CampaignPrice;

                    //Bu siparişi kaydet
                    orderList.Add(orderEntity);

                    var campaignOrderJson = JsonConvert.SerializeObject(orderList);

                    await base.WriteJson(Constants.OrderConstant.StorePath, campaignOrderJson);

                    #endregion

                    //Every order create a demand. So campaign price should increase.
                    var priceIncreaseRate = ((double)orderEntity.Quantity / (double)currentCampaign.TargetSalesCount * 0.1);

                    #region Update Campaign

                    campaignList.Remove(currentCampaign);
                    currentCampaign.TargetSalesCount -= orderEntity.Quantity;

                    campaignList.Add(currentCampaign);

                    var campaignListJson = JsonConvert.SerializeObject(campaignList);
                    await base.WriteJson(Constants.CampaignConstant.StorePath, campaignListJson);

                    #endregion

                    #region Update Product Stock and Price

                    productList.Remove(product);

                    product.CampaignPrice += Convert.ToInt32(product.CampaignPrice * priceIncreaseRate);

                    product.Stock -= orderEntity.Quantity;

                    productList.Add(product);

                    var productJson = JsonConvert.SerializeObject(productList);

                    await base.WriteJson(Constants.ProductConstant.StorePath, productJson);

                    #endregion
                }
            }
            else
            {
                //If there is no campaign. Offer at normal price
                #region Order Without Campaign

                orderEntity.CampaignCode = string.Empty;
                orderEntity.Price = product.Price;

                orderList.Add(orderEntity);
                var normalOrderJson = JsonConvert.SerializeObject(orderList);
                await base.WriteJson(Constants.OrderConstant.StorePath, normalOrderJson);

                #endregion

                #region Update Product Stock and Price

                productList.Remove(product);

                product.Stock -= orderEntity.Quantity;

                productList.Add(product);

                var productJson = JsonConvert.SerializeObject(productList);

                await base.WriteJson(Constants.ProductConstant.StorePath, productJson);

                #endregion
            }

            return ResponseHelper.GetInstance()
                    .GetResponse(Constants.OrderConstant.CreateOrderMessage, 
                        new object[] 
                            { 
                                orderEntity.ProductCode,
                                baseQuantity
                            }
                    );          
        }
    }
}
