using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository
    {
        private IMapper _mapper;
        public OrderDetailRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailList()
        {
            try
            {
                var ods = OrderDetailDAO.Instance.GetOrderDetailList();
                var orderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailObject>>(ods);
                return orderDetails;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderDetailObject GetOrderDetail(int orderId, int productId)
        {
            try
            {
                var od = OrderDetailDAO.Instance.GetOrderDetail(orderId, productId);
                var orderDetail = _mapper.Map<OrderDetail, OrderDetailObject>(od);
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailByOrderId(int id)
        {
            try
            {
                var od = OrderDetailDAO.Instance.GetOrderDetailsByOrderId(id);
                var orderDetail = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailObject>>(od);
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateOrderDetail(OrderDetailObject orderDetailObject)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetailObject, OrderDetail>(orderDetailObject);
                OrderDetailDAO.Instance.Create(orderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(OrderDetailObject orderDetailObject)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetailObject, OrderDetail>(orderDetailObject);
                OrderDetailDAO.Instance.Delete(orderDetail);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}