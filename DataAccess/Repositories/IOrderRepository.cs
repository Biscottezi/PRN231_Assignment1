using System;
using System.Collections.Generic;
using BusinessLogic;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> GetOrderList();
        IEnumerable<OrderObject> GetOrdersByDate(DateTime startDate, DateTime endDate);
        IEnumerable<OrderObject> GetOrdersByMemberId(int id);
        OrderObject GetOrderById(int id);
        void CreateOrder(OrderObject orderObject);
        void UpdateOrder(OrderObject orderObject);
        void DeleteOrder(OrderObject orderObject);
    }
}