using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Domain.Requests.OrderHeader;

namespace Kalabean.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHeaderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailsRepository;
        private readonly IOrderHeaderMapper _orderMapper;
        private readonly IOrderDetailMapper _detailMapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderHeaderRepository orderRepository,
                            IOrderDetailRepository orderDetailsRepository,
                            IOrderHeaderMapper orderMapper,
                            IOrderDetailMapper detailMapper,
                            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _orderMapper = orderMapper;
            _detailMapper = detailMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderHeaderResponse>> GetOrdersAsync(GetOrdersRequest request)
        {
            var result = await _orderRepository.Get(request);
            return result.Select(c => _orderMapper.Map(c));
        }
        public async Task<OrderHeaderResponse> GetOrderAsync(GetOrderHeaderRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Order = await _orderRepository.GetById(request.Id);
            return _orderMapper.Map(Order);
        }
        public async Task<OrderHeaderResponse> AddOrderAsync(AddOrderHeaderRequest request)
        {
            var item = _orderMapper.Map(request);
            if (request.OrderDetail != null)
            {
                item.OrderDetails = new List<Domain.Entities.OrderDetail>();
                item.OrderDetails.Add(_detailMapper.Map(request.OrderDetail));
            }
            item.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            item.OrderStatus = (byte)Domain.OrderStatus.AwaitingApproval;
            var result = _orderRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _orderMapper.Map(await _orderRepository.GetById(result.Id));
        }
        public async Task<OrderHeaderResponse> EditOrderAsync(EditOrderHeaderRequest request)
        {
            var existingRecord = await _orderRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _orderMapper.Map(request);
            var result = _orderRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _orderMapper.Map(await _orderRepository.GetById(result.Id));
        }

        public async Task BatchDeleteOrdersAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.OrderHeader> orders =
                _orderRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.OrderHeader Order in orders)
            {
                List<Kalabean.Domain.Entities.OrderDetail> orderDetails =
                _orderDetailsRepository.List(c => ids.Contains(c.OrderId)).ToList();
                foreach (var OrderDetails in orderDetails)
                {
                    Order.IsDeleted = true;
                    _orderDetailsRepository.UpdateBatch(orderDetails);
                }
                Order.IsDeleted = true;
                _orderRepository.UpdateBatch(orders);
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
