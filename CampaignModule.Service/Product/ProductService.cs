using CampaignModule.Domain.Product;
using CampaignModule.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository<ProductItem> _productRepository;

        public ProductService(IProductRepository<ProductItem> productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<string> CreateAsync(List<string> commands)
        {
            var productItem = new ProductItem( code: commands[1], price: commands[2], stock: commands[3]);

            return await _productRepository.CreateAsync(productItem);
        }

        public async Task<string> GetAsync(List<string> commands)
        {
            var product = await _productRepository.GetAsync(commands[1]);

            return ResponseHelper.GetInstance().GetResponse(Constants.ProductConstant.GetProductMessage, new object[] { product.ProductCode, product.Price, product.Stock });           
        }
    }
}
