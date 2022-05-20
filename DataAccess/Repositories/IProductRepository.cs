using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.RequestModel;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductViewModel> GetProductList(ProductSearchModel searchModel);
        IEnumerable<ProductViewModel> SearchProduct(string? keyword, int? id, decimal? price, int? stock);
        IEnumerable<ProductViewModel> GetProductByUnitInStock(int unitInStock);
        IEnumerable<ProductViewModel> GetProductByUnitPrice(decimal unitPrice);
        ProductViewModel GetProductById(int id);
        void CreateProduct(ProductCreateModel createModel);
        void UpdateProduct(int id, ProductCreateModel requestModel);
        void DeleteProduct(int id);
    }
}