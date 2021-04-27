using CampaignModule.Core.Repositories;
using CampaignModule.Domain.Campaign;
using CampaignModule.Domain.Order;
using CampaignModule.Domain.Product;
using CampaignModule.Domain.Timer;
using CampaignModule.Service.Campaign;
using CampaignModule.Service.Order;
using CampaignModule.Service.Product;
using CampaignModule.Service.Timer;
using CampaignModule.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static CampaignModule.Utilities.Constants;

namespace CampaignModule.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            CheckFolderPath();

            Console.WriteLine("--- Campaign Module Case ---");

            _ = RunModule(serviceProvider);
        }

        /// <summary>
        /// Setup DI
        /// </summary>
        /// <param name="services"></param>
        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IProductRepository<ProductItem>, ProductRepository>()
                .AddSingleton<ICampaignService, CampaignService>()
                .AddSingleton<ICampaignRepository<CampaignItem>, CampaignRepository>()
                .AddSingleton<IOrderService, OrderService>()
                .AddSingleton<IOrderRepository<OrderItem>, OrderRepository>()
                .AddSingleton<ITimerService, TimerService>()
                .AddSingleton<ITimerRepository<TimerItem>, TimerRepository>()
                .BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        /// Main module execution method.
        /// </summary>
        /// <param name="serviceProvider"></param>
        private static async Task RunModule(ServiceProvider serviceProvider)
        {
            Console.WriteLine("\nPlease enter your command:");
            var command = Console.ReadLine();

            do
            {
                try
                {
                    //Komutu işlemek lazım. 
                    if (!string.IsNullOrEmpty(command))
                    {
                        var commandParts = CommandHelper.GetInstance().PrepareCommandBase(command);

                        //Bu komutlara göre sistem bir iş yapacak. Örneğin, ürün oluşturma veya ürün görüntüleme.
                        switch (commandParts.First().ToString())
                        {
                            case ProductConstant.CreateCommand:
                                var createService = serviceProvider.GetService<IProductService>();
                                var response = await createService.CreateAsync(commandParts);

                                Console.WriteLine(response);

                                break;
                            case ProductConstant.GetCommand:
                                var getService = serviceProvider.GetService<IProductService>();
                                var getResponse = await getService.GetAsync(commandParts);

                                Console.WriteLine(getResponse);

                                break;
                            case OrderConstant.CreateCommand:
                                var orderCreateService = serviceProvider.GetService<IOrderService>();
                                var orderCreateResponse = await orderCreateService.CreateAsync(commandParts);

                                Console.WriteLine(orderCreateResponse);

                                break;
                            case CampaignConstant.CreateCommand:
                                var campaignCreateService = serviceProvider.GetService<ICampaignService>();
                                var campaignCreateResponse = await campaignCreateService.CreateAsync(commandParts);

                                Console.WriteLine(campaignCreateResponse);

                                break;
                            case CampaignConstant.GetCommand:
                                var campaignGetService = serviceProvider.GetService<ICampaignService>();
                                var campaignGetResponse = await campaignGetService.GetAsync(commandParts);

                                Console.WriteLine(campaignGetResponse);

                                break;
                            case TimerConstant.CreateCommand:
                                var timeCreateService = serviceProvider.GetService<ITimerService>();
                                var timerResponse = await timeCreateService.CreateAsync(commandParts);

                                Console.WriteLine(timerResponse);

                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\nPlease enter your command:");
                command = Console.ReadLine();
            }
            while (!command.Equals("exit"));
        }

        /// <summary>
        /// Checking folder path for storing. If not exist, it is create directory which name is HepsiStore.
        /// </summary>
        private static void CheckFolderPath()
        {
            try
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(appDataFolder, Constants.General.StoreDirectoryName);
                Directory.CreateDirectory(specificFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Store dosya yolu bulunamadi. Izinler verilmemis olabilir.", ex);
            }
        }
    }
}
