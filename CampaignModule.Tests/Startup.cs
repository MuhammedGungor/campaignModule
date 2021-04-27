using CampaignModule.Core.Repositories;
using CampaignModule.Domain.Campaign;
using CampaignModule.Domain.Order;
using CampaignModule.Domain.Product;
using CampaignModule.Domain.Timer;
using CampaignModule.Service.Campaign;
using CampaignModule.Service.Order;
using CampaignModule.Service.Product;
using CampaignModule.Service.Timer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductRepository<ProductItem>, ProductRepository>();
            services.AddSingleton<ICampaignService, CampaignService>();
            services.AddSingleton<ICampaignRepository<CampaignItem>, CampaignRepository>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderRepository<OrderItem>, OrderRepository>();
            services.AddSingleton<ITimerService, TimerService>();
            services.AddSingleton<ITimerRepository<TimerItem>, TimerRepository>();
        }
    }
}
