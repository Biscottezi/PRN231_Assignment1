using System.Collections.Generic;

namespace BusinessLogic
{
    public class ProductObject
    {
        public ProductObject()
        {
            OrderDetails = new HashSet<OrderDetailObject>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public virtual CategoryObject Category { get; set; }
        public virtual ICollection<OrderDetailObject> OrderDetails { get; set; }
    }
}