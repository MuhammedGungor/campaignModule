using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Utilities
{
    public class Constants
    {
        public static class General
        {
            public const string StoreDirectoryName = "HepsiStore";
            public const string CreateErrorMessage = "The creation process failed.";
            public const string IncreaseTimeCommand = "increase_time";
        }

        public static class ProductConstant
        {
            public const string StorePath = "\\product.json";
            public const string SameProductExistMessage = "Already have this product.";
            public const string CreateProductMessage = "Product created; code [PARAM_1], price [PARAM_2], stock [PARAM_3]";
            public const string GetProductMessage = "Product [PARAM_1] info; price [PARAM_2], stock [PARAM_3]";
            public const string ProductsEmpty = "There is no product in this store.";
            public const string ProductNotFound = "Product not found.";
            public const string CreateCommand = "create_product";
            public const string GetCommand = "get_product_info";
        }

        public static class OrderConstant
        {
            public const string StorePath = "\\order.json";
            public const string CreateCommand = "create_order";
            public const string OrderNotFound = "Order not found.";
            public const string OrdersEmpty = "There is no order in this store.";
            public const string CreateOrderMessage = "Order created; product [PARAM_1], quantity [PARAM_2]";
        }

        public static class CampaignConstant
        {
            public const string StorePath = "\\campaign.json";
            public const string CreateCommand = "create_campaign";
            public const string GetCommand = "get_campaign_info";
            public const string SameCampaignExistMessage = "Already have this campaign.";
            public const string CampaignNotFound = "Campaign not found.";
            public const string CreateProductMessage = "Campaign created; name [PARAM_1], product [PARAM_2], duration [PARAM_3], limit [PARAM_4], target sales count [PARAM_5]";
            public const string GetCampaignMessage = "Campaign [PARAM_1] info; Status [PARAM_2], Target Sales [PARAM_3], Total Sales [PARAM_4], Turnover [PARAM_5], Average Item Price [PARAM_6]";
        }
    }
}
