using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Order
{
    public interface IOrderService
    {
        Task<string> CreateAsync(List<string> commands);
    }
}
