using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO _instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            List<Product> products;
            try
            {
                var db = new FStoreDBContext();
                products = db.Products.Include(prod => prod.Category)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            try
            {
                var db = new FStoreDBContext();
                product = db.Products.Include(prod => prod.Category)
                    .SingleOrDefault(prod => prod.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public IEnumerable<Product> SearchProduct(String? keyword, int? id, decimal? price, int? stock)
        {
            List<Product> products;
            bool hasId = id != null;
            bool hasKeyword = keyword != null;
            bool hasPrice = price != null;
            bool hasStock = stock != null;
            System.Linq.Expressions.Expression<Func<Product, bool>> expression;

            if (hasId)
            {
                expression = p => p.ProductId == id;
            }
            else if (hasKeyword)
            {
                expression = p => p.ProductName.Contains(keyword);
            }
            else if (hasPrice)
            {
                expression = p => p.UnitPrice == price;
            }
            else if (hasStock)
            {
                expression = p => p.UnitsInStock == stock;
            }
            else if (hasKeyword && hasPrice)
            {
                expression = p => p.ProductName.Contains(keyword) && p.UnitPrice == price;
            }
            else if(hasKeyword && hasStock)
            {
                expression = p => p.ProductName.Contains(keyword) && p.UnitsInStock == stock;
            }
            else if(hasPrice && hasStock)
            {
                expression = p => p.UnitsInStock == stock && p.UnitPrice == price;
            }
            else
            {
                expression = p => p.ProductName
                    .Contains(keyword) && p.UnitPrice == price && p.UnitsInStock == stock;
            }
             
            try
            {
                var db = new FStoreDBContext();
                products = db.Products.Where(expression)
                    .Include(prod => prod.Category)
                    .ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public IEnumerable<Product> GetProductByUnitInStock(int unitInStock)
        {
            List<Product> products;
            try
            {
                var db = new FStoreDBContext();
                products = db.Products.Where(p => p.UnitsInStock == unitInStock)
                    .Include(prod => prod.Category)
                    .ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public IEnumerable<Product> GetProductByUnitPrice(decimal unitPrice)
        {
            List<Product> products;
            try
            {
                var db = new FStoreDBContext();
                products = db.Products.Where(p => p.UnitPrice == unitPrice)
                    .Include(prod => prod.Category)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public void Create(Product product)
        {
            try
            {
                var prod = GetProductById(product.ProductId);
                if (prod == null)
                {
                    var db = new FStoreDBContext();
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This product already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                var prod = GetProductById(product.ProductId);
                if (prod != null)
                {
                    var db = new FStoreDBContext();
                    db.Entry<Product>(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This product doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var prod = GetProductById(id);
                if (prod != null)
                {
                    var db = new FStoreDBContext();
                    db.Products.Remove(prod);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("This product doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}