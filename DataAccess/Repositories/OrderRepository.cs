using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        public OrderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<OrderViewModel> GetOrderList(OrderSortModel sortDate)
        {
            try
            {
                IEnumerable<Order> ords;
                if (sortDate.startDate != null && sortDate.endDate != null)
                {
                    ords = OrderDAO.Instance.GetOrdersByDate((DateTime)sortDate.startDate, (DateTime)sortDate.endDate);
                }
                else
                {
                    ords = OrderDAO.Instance.GetOrderList();
                }
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(ords);
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderViewModel> GetOrdersByMemberId(int id)
        {
            try
            {
                var ords = OrderDAO.Instance.GetOrdersByMemberId(id);
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(ords);
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderViewModel> GetOrdersByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var ords = OrderDAO.Instance.GetOrdersByDate(startDate, endDate);
                var orders = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(ords);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderViewModel GetOrderById(int id)
        {
            try
            {
                var ord = OrderDAO.Instance.GetOrderById(id);
                var order = _mapper.Map<Order, OrderViewModel>(ord);
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateOrder(OrderCreateModel createModel)
        {
            try
            {
                var order = _mapper.Map<OrderCreateModel, Order>(createModel);
                OrderDAO.Instance.Create(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(int id, OrderCreateModel requestModel)
        {
            try
            {
                var order = _mapper.Map<OrderCreateModel, Order>(requestModel);
                order.OrderId = id;
                OrderDAO.Instance.Update(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(int id)
        {
            try
            {
                OrderDAO.Instance.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}