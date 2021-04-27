using CampaignModule.Service.Product;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CampaignModule.Tests.Service
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private readonly List<string> productCreateCommand = new List<string> { "create_product","PTest","10","100" };
        private readonly List<string> productGetCommand = new List<string> { "get_product_info", "PTest"};

        public ProductServiceTest(IProductService productService)
        {
            this._productService = productService;
        }

        [Fact]
        public void Should_Insert_A_Product()
        {
            var response = _productService.CreateAsync(productCreateCommand);

            response.Wait();

            var expectedResponse = ResponseHelper.GetInstance().GetResponse(Constants.ProductConstant.CreateProductMessage, new object[] { productCreateCommand[1], productCreateCommand[2], productCreateCommand[3] });

            Assert.Equal(response.Result, expectedResponse);
        }

        [Fact]
        public void Should_Get_A_Product()
        {
            var response = _productService.GetAsync(productGetCommand);

            response.Wait();

            Assert.NotEmpty(response.Result);
        }
    }
}
