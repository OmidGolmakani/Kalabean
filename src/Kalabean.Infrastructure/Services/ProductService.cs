using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;

namespace Kalabean.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IProductImageRepository _ProductImageRepository;
        private readonly IProductMapper _ProductMapper;
        private readonly IProductImageMapper _ProductImageMapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository ProductRepository,
                              IProductImageRepository ProductImageRepository,
                              IProductMapper ProductMapper,
                              IProductImageMapper ProductImageMapper,
                              IUnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            _ProductImageRepository = ProductImageRepository;
            _ProductMapper = ProductMapper;
            _ProductImageMapper = ProductImageMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var result = await _ProductRepository.Get();
            return result.Select(p => _ProductMapper.Map(p));
        }
        public async Task<ProductResponse> GetProductAsync(GetProductRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Product = await _ProductRepository.GetById(request.Id);
            return _ProductMapper.Map(Product);
        }
        public async Task<ProductResponse> AddProductAsync(AddProductRequest request)
        {
            var item = _ProductMapper.Map(request);
            var result = _ProductRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _ProductMapper.Map(await _ProductRepository.GetById(result.Id));
        }
        public async Task<ProductResponse> EditProductAsync(EditProductRequest request)
        {
            var existingRecord = await _ProductRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _ProductMapper.Map(request);
            var result = _ProductRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _ProductMapper.Map(await _ProductRepository.GetById(result.Id));
        }

        public async Task BatchDeleteProductsAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.Product> Products =
                _ProductRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Product Product in Products)
                Product.IsDeleted = true;
            _ProductRepository.UpdateBatch(Products);

            await _unitOfWork.CommitAsync();
        }
    }
}
