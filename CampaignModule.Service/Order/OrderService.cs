

using CampaignModule.Domain.Order;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignModule.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository<OrderItem> _orderRepository;

        public OrderService(IOrderRepository<OrderItem> orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task<string> CreateAsync(List<string> commands)
        {
            try
            {
                var orderItem = new OrderItem(code: commands[1], quantity: commands[2]);

                return await _orderRepository.CreateAsync(orderItem);
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.General.CreateErrorMessage);
            }
        }
    }
}
