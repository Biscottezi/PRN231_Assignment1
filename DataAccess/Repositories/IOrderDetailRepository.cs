using System.Collections.Generic;
using BusinessLogic;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> GetOrderDetailList();
        IEnumerable<OrderDetailObject> GetOrderDetailByOrderId(int id);
        OrderDetailObject GetOrderDetail(int orderId, int productId);
        void CreateOrderDetail(OrderDetailObject orderDetailObject);
        void DeleteOrderDetail(OrderDetailObject orderDetailObject);
    }
}