using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.DataAccess;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IMapper _mapper;
        public OrderDetailRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<OrderDetailViewModel> GetOrderDetailList()
        {
            try
            {
                var ods = OrderDetailDAO.Instance.GetOrderDetailList();
                var orderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(ods);
                return orderDetails;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderDetailViewModel GetOrderDetail(int orderId, int productId)
        {
            try
            {
                var od = OrderDetailDAO.Instance.GetOrderDetail(orderId, productId);
                var orderDetail = _mapper.Map<OrderDetail, OrderDetailViewModel>(od);
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<OrderDetailViewModel> GetOrderDetailByOrderId(int id)
        {
            try
            {
                var od = OrderDetailDAO.Instance.GetOrderDetailsByOrderId(id);
                var orderDetail = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(od);
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateOrderDetail(OrderDetailCreateModel createModel)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetailCreateModel, OrderDetail>(createModel);
                OrderDetailDAO.Instance.Create(orderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(int orderId, int productId)
        {
            try
            {
                OrderDetailDAO.Instance.Delete(orderId, productId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}