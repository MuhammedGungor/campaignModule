using CampaignModule.Service.Campaign;
using CampaignModule.Service.Product;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CampaignModule.Tests.Service
{
    public class CampaignServiceTest
    {
        private readonly ICampaignService _campaignService;
        private readonly IProductService _productService;
        private readonly List<string> createCommand = new List<string>();
        private readonly List<string> getCommand = new List<string> { "get_campaign_info", "CTest" };

        public CampaignServiceTest(ICampaignService campaignService, IProductService productService)
        {
            this._campaignService = campaignService;
            this._productService = productService;

            var randomGnr = new Random().Next(1, 99999);
            this.createCommand.AddRange(new List<string> { "create_campaign", $"TestCampaign_{randomGnr}", "PTest", "10", "20", "100" });
        }

        [Fact]
        public void Should_Insert_A_Campaign()
        {
            var productListResponse = _productService.GetAll();

            productListResponse.Wait();

            var randomProduct = productListResponse.Result.FirstOrDefault();

            if (randomProduct == null)
            {
                Assert.True(randomProduct != null,Constants.ProductConstant.ProductNotFound);
            }

            createCommand[2] = randomProduct.ProductCode;

            var response = _campaignService.CreateAsync(createCommand);

            response.Wait();

            var expectedResponse = ResponseHelper.GetInstance().GetResponse(Constants.CampaignConstant.CreateProductMessage, 
                new object[] 
                { 
                    createCommand[1], 
                    createCommand[2], 
                    createCommand[3], 
                    createCommand[4], 
                    createCommand[5] 
                });

            Assert.Equal(response.Result, expectedResponse);
        }

        [Fact]
        public void Should_Get_A_Campaign()
        {
            var response = _campaignService.GetAsync(getCommand);

            response.Wait();

            Assert.NotEmpty(response.Result);
        }
    }
}
