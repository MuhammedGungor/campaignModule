using CampaignModule.Service.Order;
using CampaignModule.Service.Product;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CampaignModule.Tests.Service
{
    public class OrderServiceTest
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly List<string> createCommand = new List<string>();

        public OrderServiceTest(IProductService productService, IOrderService orderService)
        {
            this._productService = productService;
            this._orderService = orderService;

            var randomGnr = new Random().Next(1, 99999);

            this.createCommand.AddRange(new List<string> { "create_order", $"TestProduct", "1"});            
        }

        [Fact]
        public void Should_Create_An_Order()
        {
            var productAwait = _productService.GetAll();

            productAwait.Wait();

            var product = productAwait.Result.FirstOrDefault();

            createCommand[1] = product.ProductCode;

            var response = _orderService.CreateAsync(createCommand);

            response.Wait();

            var expectedResponse = ResponseHelper.GetInstance()
                    .GetResponse(Constants.OrderConstant.CreateOrderMessage,
                        new object[]
                            {
                                createCommand[1],
                                createCommand[2]
                            }
                    );

            Assert.Equal(response.Result, expectedResponse);
        }
    }
}
