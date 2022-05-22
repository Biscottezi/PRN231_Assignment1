using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataAccess;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO _instance = null;
        private static readonly object InstanceLock = new object();

        private CategoryDAO()
        {
        }

        public static CategoryDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CategoryDAO();
                    }

                    return _instance;
                }
            }
        }

        public IEnumerable<Category> GetCategoryList()
        {
            List<Category> categories;
            try
            {
                var db = new FStoreDBContext();
                categories = db.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return categories;
        }
    }
}