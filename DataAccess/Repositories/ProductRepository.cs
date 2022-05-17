using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class ProductRepository
    {
        private IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<ProductObject> GetProductList()
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductList();
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductObject>>(prods);
                return products;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<ProductObject> SearchProduct(string? keyword, int? id, decimal? price, int? stock)
        {
            try
            {
                var prods = ProductDAO.Instance.SearchProduct(keyword, id, price, stock);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductObject>>(prods);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductObject> GetProductByUnitInStock(int unitInStock)
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductByUnitInStock(unitInStock);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductObject>>(prods);
                return products;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ProductObject> GetProductByUnitPrice(decimal unitPrice)
        {
            try
            {
                var prods = ProductDAO.Instance.GetProductByUnitPrice(unitPrice);
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductObject>>(prods);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductObject GetProductById(int id)
        {
            try
            {
                var prod = ProductDAO.Instance.GetProductById(id);
                var product = _mapper.Map<Product, ProductObject>(prod);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreateProduct(ProductObject productObject)
        {
            try
            {
                var product = _mapper.Map<ProductObject, Product>(productObject);
                ProductDAO.Instance.Create(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(ProductObject productObject)
        {
            try
            {
                var product = _mapper.Map<ProductObject, Product>(productObject);
                ProductDAO.Instance.Update(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteProduct(ProductObject productObject)
        {
            try
            {
                var product = _mapper.Map<ProductObject, Product>(productObject);
                ProductDAO.Instance.Delete(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}