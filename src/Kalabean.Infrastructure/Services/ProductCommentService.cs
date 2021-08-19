using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.ProductComment;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;

namespace Kalabean.Infrastructure.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IProductCommentRepository _ProductCommentRepository;
        private readonly IProductCommentMapper _ProductCommentMapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductCommentService(IProductCommentRepository ProductCommentRepository,
            IProductCommentMapper ProductCommentMapper,
            IUnitOfWork unitOfWork)
        {
            _ProductCommentRepository = ProductCommentRepository;
            _ProductCommentMapper = ProductCommentMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListPagingResponse<ProductCommentResponse>> GetProductCommentsAsync(GetProductCommentsRequest request)
        {
            var result = await _ProductCommentRepository.Get(request);
            var list = result.Select(c => _ProductCommentMapper.Map(c));
            var count = await _ProductCommentRepository.Count(request);
            return new ListPagingResponse<ProductCommentResponse>() { Items = list, Total = count };
        }
        public async Task<ProductCommentResponse> GetProductCommentAsync(GetProductCommentRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var ProductComment = await _ProductCommentRepository.GetById(request.Id);
            return _ProductCommentMapper.Map(ProductComment);
        }
        public async Task<ProductCommentResponse> AddProductCommentAsync(AddProductCommentRequest request)
        {
            var item = _ProductCommentMapper.Map(request);
            if (Helpers.JWTTokenManager.GetUserIdByToken() != -1)
            {
                item.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            }
            var result = _ProductCommentRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _ProductCommentMapper.Map(await _ProductCommentRepository.GetById(result.Id));
        }
        public async Task<ProductCommentResponse> EditProductCommentAsync(EditProductCommentRequest request)
        {
            var existingRecord = await _ProductCommentRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _ProductCommentMapper.Map(request);
            if (Helpers.JWTTokenManager.GetUserIdByToken() != -1)
            {
                entity.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            }
            var result = _ProductCommentRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _ProductCommentMapper.Map(await _ProductCommentRepository.GetById(result.Id));
        }

        public async Task BatchDeleteProductCommentsAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.ProductComment> ProductsComments =
                _ProductCommentRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.ProductComment ProductComment in ProductsComments)
                ProductComment.IsDeleted = true;
            _ProductCommentRepository.UpdateBatch(ProductsComments);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateProductCommentStatusAsync(EditProductCommnetStatusRequest request)
        {
            if (request.Status == Domain.Entities.ProductCommentService.AwaitingApproval)
            {
                throw new ArgumentException($"Cannot set {request.Status} Status");
            }
            Kalabean.Domain.Entities.ProductComment existingRecord =
               await _ProductCommentRepository.GetById(request.Id);
            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");
            existingRecord.Status = (byte)request.Status;
            existingRecord.DatePublished = DateTime.Now;
            existingRecord.AdminId = Helpers.JWTTokenManager.GetUserIdByToken();
            _ProductCommentRepository.Update(existingRecord);
            await _unitOfWork.CommitAsync();
        }
    }
}
