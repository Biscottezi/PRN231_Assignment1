using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<ProductViewModel> GetProductList(ProductSearchModel searchModel)
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductList().ToList();
                
                if (searchModel.UnitPrice > 0)
                {
                    prods = prods.Where(p => p.UnitPrice == searchModel.UnitPrice)
                        .ToList();
                }
                
                if (searchModel.ProductName is { Length: > 0 })
                {
                    prods = prods.Where(p => p.ProductName.Contains(searchModel.ProductName))
                        .ToList();
                }
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(prods);
                return products;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ProductViewModel> SearchProduct(string? keyword, int? id, decimal? price, int? stock)
        {
            try
            {
                var prods = ProductDAO.Instance.SearchProduct(keyword, id, price, stock);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(prods);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductViewModel> GetProductByUnitInStock(int unitInStock)
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductByUnitInStock(unitInStock);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(prods);
                return products;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductViewModel> GetProductByUnitPrice(decimal unitPrice)
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductByUnitPrice(unitPrice);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(prods);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductViewModel GetProductById(int id)
        {
            try
            {
                var prod = ProductDAO.Instance.GetProductById(id);
                var product = _mapper.Map<Product, ProductViewModel>(prod);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreateProduct(ProductCreateModel createModel)
        {
            try
            {
                var product = _mapper.Map<ProductCreateModel, Product>(createModel);
                ProductDAO.Instance.Create(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(int id, ProductCreateModel requestModel)
        {
            try
            {
                var product = _mapper.Map<ProductCreateModel, Product>(requestModel);
                product.ProductId = id;
                ProductDAO.Instance.Update(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteProduct(int id)
        {
            try
            {
                ProductDAO.Instance.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}