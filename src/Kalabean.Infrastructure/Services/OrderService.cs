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
        private readonly IOrderHeaderRepository _OrderRepository;
        private readonly IOrderHeaderMapper _OrderMapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderHeaderRepository OrderRepository,
            IOrderHeaderMapper OrderMapper,
            IUnitOfWork unitOfWork)
        {
            _OrderRepository = OrderRepository;
            _OrderMapper = OrderMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderHeaderResponse>> GetOrdersAsync()
        {
            var result = await _OrderRepository.Get();
            return result.Select(c => _OrderMapper.Map(c));
        }
        public async Task<OrderHeaderResponse> GetOrderAsync(GetOrderHeaderRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Order = await _OrderRepository.GetById(request.Id);
            return _OrderMapper.Map(Order);
        }
        public async Task<OrderHeaderResponse> AddOrderAsync(AddOrderHeaderRequest request)
        {
            var item = _OrderMapper.Map(request);
            var result = _OrderRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _OrderMapper.Map(await _OrderRepository.GetById(result.Id));
        }
        public async Task<OrderHeaderResponse> EditOrderAsync(EditOrderHeaderRequest request)
        {
            var existingRecord = await _OrderRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _OrderMapper.Map(request);
            var result = _OrderRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _OrderMapper.Map(await _OrderRepository.GetById(result.Id));
        }

        public async Task BatchDeleteOrdersAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.OrderHeader> orders =
                _OrderRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.OrderHeader Order in orders)
                Order.IsDeleted = true;
            _OrderRepository.UpdateBatch(orders);

            await _unitOfWork.CommitAsync();
        }
    }
}
