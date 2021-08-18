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
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;

namespace Kalabean.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHeaderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailsRepository;
        private readonly IOrderHeaderMapper _orderMapper;
        private readonly IOrderDetailMapper _detailMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly List<ImageSize> _imageConfig;

        public OrderService(IOrderHeaderRepository orderRepository,
                            IOrderDetailRepository orderDetailsRepository,
                            IOrderHeaderMapper orderMapper,
                            IOrderDetailMapper detailMapper,
                            IUnitOfWork unitOfWork,
                            IFileAccessProvider fileProvider,
                            IResizeImageService<long> resizeImageService,
                            IOptions<ImageSize> ImageConfig)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _orderMapper = orderMapper;
            _detailMapper = detailMapper;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
            this._resizeImageService = resizeImageService;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Order).ToList();
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
            Tuple<bool, string> ImgResult = null;
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    ImgResult = _fileProvider.SaveOrderImage(fileContent, result.Id);

                foreach (var ImageResize in _imageConfig)
                {

                    if (ImgResult != null && ImgResult.Item1)
                    {
                        await _resizeImageService.Resize(new GetImageRequest<long>()
                        {
                            Id = result.Id,
                            ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                            ImageUrl = ImgResult.Item2,
                            Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                        });
                    }
                }
            }

            return _orderMapper.Map(await _orderRepository.GetById(result.Id));
        }
        public async Task<OrderHeaderResponse> EditOrderAsync(EditOrderHeaderRequest request)
        {
            var existingRecord = await _orderRepository.GetById(request.id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.id} is not present");

            var entity = _orderMapper.Map(request);
            entity.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            if (request.OrderDetail != null)
            {
                entity.OrderDetails = new List<Domain.Entities.OrderDetail>() { _detailMapper.Map(request.OrderDetail) };
                entity.OrderDetails.FirstOrDefault().OrderId = entity.Id;
                entity.OrderDetails.FirstOrDefault().Id =
                    _orderDetailsRepository.GetByOrderId(entity.Id).Result.Id;
            }
            if (request.ImageEdited)
            {
                if (entity.HasImage || request.Image != null)
                {
                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveOrderImage(fileContent, entity.Id);
                        entity.HasImage = true;

                        foreach (var ImageResize in _imageConfig)
                        {
                            if (ImgResult!=null && ImgResult.Item1)
                            {
                                await _resizeImageService.Resize(new GetImageRequest<long>()
                                {
                                    Id = existingRecord.Id,
                                    ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                                    ImageUrl = ImgResult.Item2,
                                    Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                                });
                            }
                        }
                    }
                    
                }
                else
                {
                    _fileProvider.DeleteOrderImage(entity.Id);
                    entity.HasImage = false;
                }
            }
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
