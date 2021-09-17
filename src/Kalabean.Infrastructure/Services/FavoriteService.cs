using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Favorites;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;

namespace Kalabean.Infrastructure.Services
{
    public class FavoritesService : IFavoriteService
    {
        private readonly IFavoriteRepository _FavoritesRepository;
        private readonly IFavoriteMapper _FavoritesMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IStoreMapper _storeMapper;

        public FavoritesService(IFavoriteRepository FavoritesRepository,
                                IFavoriteMapper FavoritesMapper,
                                IUnitOfWork unitOfWork,
                                IProductRepository productRepository,
                                IProductMapper productMapper,
                                IStoreRepository storeRepository,
                                IStoreMapper storeMapper)
        {
            _FavoritesRepository = FavoritesRepository;
            _FavoritesMapper = FavoritesMapper;
            _unitOfWork = unitOfWork;
            this._productRepository = productRepository;
            this._productMapper = productMapper;
            this._storeRepository = storeRepository;
            this._storeMapper = storeMapper;
        }

        public async Task<ListPagingResponse<FavoriteResponse>> GetFavoritesAsync(GetFavoritesRequest request)
        {
            var fave = await _FavoritesRepository.Get(request);
            int count = 0;
            IEnumerable<FavoriteResponse> favorites = null;
            IQueryable<Domain.Entities.Product> products = null;
            IEnumerable<Domain.Entities.Store> stores = null;
            Domain.Entities.FavoriteType type = (Domain.Entities.FavoriteType)request.TypeId;
            switch (type)
            {
                case Domain.Entities.FavoriteType.Product:
                    ///GetProducts
                    products = await _productRepository.Get(new Domain.Requests.Product.GetProductsRequest()
                    {
                        Ids = fave.Select(f => f.RelatedId).ToArray()
                    });
                    ///Get Product Count
                    count = await _productRepository.Count(new Domain.Requests.Product.GetProductsRequest()
                    {
                        Ids = fave.Select(f => f.RelatedId).ToArray()
                    });
                    ///Join Products And Favorites
                    favorites = (from f in fave
                                 join p in products on f.RelatedId equals p.Id
                                 select new FavoriteResponse()
                                 {
                                     Id = f.Id,
                                     RelatedId = f.RelatedId,
                                     TypeId = f.TypeId,
                                     RelatedInfo = _productMapper.Map(p)
                                 });
                    break;
                case Domain.Entities.FavoriteType.Store:
                    ///Get Stores
                    await _storeRepository.Get(new Domain.Requests.Store.GetStoresRequest()
                    {
                        Ids = fave.Select(f => Convert.ToInt32(f.RelatedId)).ToArray()
                    });
                    ///Get Store Count
                    count = await _storeRepository.Count(new Domain.Requests.Store.GetStoresRequest()
                    {
                        Ids = fave.Select(f => Convert.ToInt32(f.RelatedId)).ToArray()
                    });
                    ///Join Stores And Favorites
                    favorites = (from f in fave
                                 join s in stores on f.RelatedId equals Convert.ToInt64(s.Id)
                                 select new FavoriteResponse()
                                 {
                                     Id = f.Id,
                                     RelatedId = f.RelatedId,
                                     TypeId = f.TypeId,
                                     RelatedInfo = _storeMapper.Map(s)
                                 });
                    break;
                default:
                    break;
            }
            return new ListPagingResponse<FavoriteResponse>() { Items = favorites, Total = count };
        }
        public async Task<FavoriteResponse> GetFavoriteAsync(GetFavoriteRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Favorites = await _FavoritesRepository.GetById(request.Id);
            return _FavoritesMapper.Map(Favorites);
        }
        public async Task<FavoriteResponse> AddFavoriteAsync(AddFavoriteRequest request)
        {
            var item = _FavoritesMapper.Map(request);
            item.UserId = Helpers.JWTTokenManager.GetUserIdByCookie();
            var result = _FavoritesRepository.Add(item);
            await _unitOfWork.CommitAsync();
            return await this.GetFavoriteAsync(new GetFavoriteRequest { Id = result.Id });
        }

        public async Task BatchDeleteFavoritesAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.Favorites> Favoritess =
                _FavoritesRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Favorites Favorites in Favoritess)
                Favorites.IsDeleted = true;
            _FavoritesRepository.UpdateBatch(Favoritess);

            await _unitOfWork.CommitAsync();
        }

        public async Task<long> Count(GetFavoritesRequest request)
        {
            if (request == null) throw new ArgumentNullException();
            var Favorites = await _FavoritesRepository.Count(request);
            return Favorites;
        }
    }
}
