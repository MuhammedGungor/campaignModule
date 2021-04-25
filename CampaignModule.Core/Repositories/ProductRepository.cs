using CampaignModule.Domain.Base;
using CampaignModule.Domain.Product;
using CampaignModule.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public class ProductRepository : BaseOperationService, IProductRepository<ProductItem>
    {
        public async Task<string> CreateAsync(ProductItem entity)
        {
            var productList = await base.GetValuesFromFolder<List<ProductItem>>(Constants.ProductConstant.StorePath);

            if (productList != null && productList.Exists(c => c.ProductCode.Equals(entity.ProductCode)))
                throw new Exception(Constants.ProductConstant.SameProductExistMessage);

            if (productList == null)
                productList = new List<ProductItem>();

            productList.Add(entity);

            var jsonable = JsonConvert.SerializeObject(productList);

            var createResponse = await base.WriteJson(Constants.ProductConstant.StorePath, jsonable);

            if (createResponse)
                return ResponseHelper.GetInstance().GetResponse(Constants.ProductConstant.CreateProductMessage, new object[] { entity.ProductCode, entity.Price, entity.Stock });
            else
                throw new Exception(Constants.General.CreateErrorMessage);
        }

        public async Task<string> GetAsync(string id)
        {
            var productList = await base.GetValuesFromFolder<List<ProductItem>>(Constants.ProductConstant.StorePath);

            if (productList != null && productList.Count == 0)
                throw new Exception(Constants.ProductConstant.ProductsEmpty);

            var product = productList.First(c => c.ProductCode == id);

            if (product == null)
                throw new Exception(Constants.ProductConstant.ProductNotFound);
            
            return ResponseHelper.GetInstance().GetResponse(Constants.ProductConstant.GetProductMessage, new object[] { product.ProductCode, product.Price, product.Stock });
        }
    }
}
