using System.Collections.Generic;

namespace BusinessLogic
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryName { get; set; }
    }
}