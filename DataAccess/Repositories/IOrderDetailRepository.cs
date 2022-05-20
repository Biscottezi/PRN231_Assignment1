using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.RequestModel;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailViewModel> GetOrderDetailList();
        IEnumerable<OrderDetailViewModel> GetOrderDetailByOrderId(int id);
        OrderDetailViewModel GetOrderDetail(int orderId, int productId);
        void CreateOrderDetail(OrderDetailCreateModel createModel);
        void DeleteOrderDetail(int orderId, int productId);
    }
}