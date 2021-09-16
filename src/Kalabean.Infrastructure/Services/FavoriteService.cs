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
        private readonly KalabeanFileProvider _fileProvider;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly List<ImageSize> _imageConfig;
        public FavoritesService(IFavoriteRepository FavoritesRepository,
                                IFavoriteMapper FavoritesMapper,
                                IUnitOfWork unitOfWork)
        {
            _FavoritesRepository = FavoritesRepository;
            _FavoritesMapper = FavoritesMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListPagingResponse<FavoriteResponse>> GetFavoritesAsync(GetFavoritesRequest request)
        {
            var result = await _FavoritesRepository.Get(request);
            var list = result.Select(c => _FavoritesMapper.Map(c));
            var count = await _FavoritesRepository.Count(request);
            return new ListPagingResponse<FavoriteResponse>() { Items = list, Total = count };
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
            return  await this.GetFavoriteAsync(new GetFavoriteRequest { Id = result.Id });
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
