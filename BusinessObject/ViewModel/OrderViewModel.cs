using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDetails = new HashSet<OrderDetailViewModel>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public virtual ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}