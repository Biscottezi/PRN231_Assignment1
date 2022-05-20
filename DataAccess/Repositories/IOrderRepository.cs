using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.RequestModel;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderViewModel> GetOrderList(OrderSortModel sortDate);
        IEnumerable<OrderViewModel> GetOrdersByDate(DateTime startDate, DateTime endDate);
        IEnumerable<OrderViewModel> GetOrdersByMemberId(int id);
        OrderViewModel GetOrderById(int id);
        void CreateOrder(OrderCreateModel createModel);
        void UpdateOrder(int id, OrderCreateModel requestModel);
        void DeleteOrder(int id);
    }
}