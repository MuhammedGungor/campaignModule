using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Domain.Campaign
{
    /// <summary>
    /// Represents campaigns
    /// </summary>
    public class CampaignItem
    {
        public CampaignItem()
        {

        }

        public CampaignItem(string name, string code, string duration, string priceManipulationLimit, string targetSalesCount,bool status = false)
        {
            this.Name = name;
            this.ProductCode = code;
            this.Duration = Convert.ToInt32(duration);
            this.PriceManipulationLimit = Convert.ToInt32(priceManipulationLimit);
            this.TargetSalesCount = Convert.ToInt32(targetSalesCount);
            this.Status = status;
        }

        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public bool Status { get; set; }
        public int Turnover { get; set; }
        public int TotalSales { get; set; }
        public int AverageItemPrice { get; set; }
    }
}
