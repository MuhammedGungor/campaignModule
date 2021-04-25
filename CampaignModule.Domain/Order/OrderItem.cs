using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Order
{
    /// <summary>
    /// Represents an order item.
    /// </summary>
    public class OrderItem
    {
        public OrderItem()
        {

        }

        public OrderItem(string code, string quantity)
        {
            this.ProductCode = code;
            this.Quantity = Convert.ToInt32(quantity);
        }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public string CampaignCode { get; set; }

        public int Price { get; set; }
    }
}
