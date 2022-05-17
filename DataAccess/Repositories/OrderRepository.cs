using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class OrderRepository
    {
        private IMapper _mapper;
        public OrderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<OrderObject> GetOrderList()
        {
            try
            {
                var ords = OrderDAO.Instance.GetOrderList();
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderObject>>(ords);
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderObject> GetOrdersByMemberId(int id)
        {
            try
            {
                var ords = OrderDAO.Instance.GetOrdersByMemberId(id);
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderObject>>(ords);
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderObject> GetOrdersByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var ords = OrderDAO.Instance.GetOrdersByDate(startDate, endDate);
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderObject>>(ords);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderObject GetOrderById(int id)
        {
            try
            {
                var ord = OrderDAO.Instance.GetOrderById(id);
                var order = _mapper.Map<Order, OrderObject>(ord);
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateOrder(OrderObject orderObject)
        {
            try
            {
                var order = _mapper.Map<OrderObject, Order>(orderObject);
                OrderDAO.Instance.Create(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(OrderObject orderObject)
        {
            try
            {
                var order = _mapper.Map<OrderObject, Order>(orderObject);
                OrderDAO.Instance.Update(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(OrderObject orderObject)
        {
            try
            {
                var order = _mapper.Map<OrderObject, Order>(orderObject);
                OrderDAO.Instance.Delete(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}