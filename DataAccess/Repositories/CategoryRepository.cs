using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper mapper;

        public CategoryRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<CategoryViewModel> GetCategoryList()
        {
            IEnumerable<CategoryViewModel> categories = null;
            try
            {
                var cats = CategoryDAO.Instance.GetCategoryList();
                categories = mapper.Map<IEnumerable<CategoryViewModel>>(cats);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return categories;
        }
    }
}