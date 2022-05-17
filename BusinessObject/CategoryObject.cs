using System.Collections.Generic;

namespace BusinessLogic
{
    public class CategoryObject
    {
        public CategoryObject()
        {
            Products = new HashSet<ProductObject>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductObject> Products { get; set; }
    }
}