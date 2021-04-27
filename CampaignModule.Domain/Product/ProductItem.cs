using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Product
{
    /// <summary>
    /// Represents product item.
    /// </summary>
    public class ProductItem
    {
        public ProductItem()
        {

        }

        public ProductItem(string code, string price, string stock)
        {
            this.Price = Convert.ToInt32(price);
            this.ProductCode = code;
            this.Stock = Convert.ToInt32(stock);
            this.CampaignPrice = Convert.ToInt32(price);
        }

        public string ProductCode { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int CampaignPrice { get; set; }
    }
}
