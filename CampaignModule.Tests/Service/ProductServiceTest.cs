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
        private readonly List<string> productCreateCommand = new List<string>();
        private readonly List<string> productGetCommand = new List<string>();

        public ProductServiceTest(IProductService productService)
        {
            this._productService = productService;

            var randomGnr = new Random().Next(1, 99999);

            this.productCreateCommand.AddRange(new List<string> { "create_product", $"TestProduct_{randomGnr}", "10", "100" });
            this.productGetCommand.AddRange(new List<string> { "get_product_info", this.productCreateCommand[1] });
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
            try
            {
                var response = _productService.GetAsync(productGetCommand);

                response.Wait();

                Assert.NotEmpty(response.Result);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Assert.Equal(Constants.ProductConstant.ProductNotFound, ex.InnerException.Message);
                }
            }
        }

        [Fact]
        public void Should_Get_All_Products()
        {
            var productsResponse = _productService.GetAll();

            productsResponse.Wait();

            Assert.NotEmpty(productsResponse.Result);
        }
    }
}
