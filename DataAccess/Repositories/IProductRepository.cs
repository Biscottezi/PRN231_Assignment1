using System.Collections.Generic;
using BusinessLogic;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProductList();
        IEnumerable<ProductObject> SearchProduct(string? keyword, int? id, decimal? price, int? stock);
        IEnumerable<ProductObject> GetProductByUnitInStock(int unitInStock);
        IEnumerable<ProductObject> GetProductByUnitPrice(decimal unitPrice);
        ProductObject GetProductById(int id);
        void CreateProduct(ProductObject productObject);
        void UpdateProduct(ProductObject productObject);
        void DeleteProduct(ProductObject productObject);
    }
}