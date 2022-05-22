using System.Collections.Generic;
using BusinessLogic;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryViewModel> GetCategoryList();
    }
}