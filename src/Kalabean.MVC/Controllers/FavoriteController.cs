using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.Controllers
{
    [Route("Favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favorite;

        public FavoriteController(IFavoriteService favorite)
        {
            this._favorite = favorite;
        }
        [HttpGet("Product")]
        public async Task<IActionResult> Product()
        {
            var model = new Models.FavoriteViewModel();
            var list = await _favorite.GetFavoritesAsync(new Domain.Requests.Favorites.GetFavoritesRequest()
            {
                TypeId = (byte)Domain.Entities.FavoriteType.Product,
                UserId = Infrastructure.Helpers.JWTTokenManager.GetUserIdByCookie()
            });
            model = (from p in list.Items
                     select new Models.FavoriteViewModel()
                     {
                         TypeId = p.TypeId,
                         RelatedInfo = p.RelatedInfo
                     }).FirstOrDefault();
            return View(model);
        }
        [HttpGet("Store")]
        public async Task<IActionResult> Store()
        {
            var model = new Models.FavoriteViewModel();
            var list = await _favorite.GetFavoritesAsync(new Domain.Requests.Favorites.GetFavoritesRequest()
            {
                TypeId = (byte)Domain.Entities.FavoriteType.Store,
                UserId = Infrastructure.Helpers.JWTTokenManager.GetUserIdByCookie()
            });
            model = (from p in list.Items
                     select new Models.FavoriteViewModel()
                     {
                         TypeId = p.TypeId,
                         RelatedInfo = p.RelatedInfo
                     }).FirstOrDefault();
            return View(model);
        }
        [HttpPost("Product")]
        public async Task<IActionResult> ProductFavorite(long RelatedId, long? Id)
        {
            var existingRecord = await _favorite.GetFavoriteAsync(new Domain.Requests.Favorites.GetFavoriteRequest()
            {
                Id = (Id ?? 0)
            });
            if (existingRecord == null)
            {
                await _favorite.AddFavoriteAsync(new Domain.Requests.Favorites.AddFavoriteRequest()
                {
                    TypeId = (byte)Domain.Entities.FavoriteType.Product,
                    RelatedId = RelatedId
                });
            }
            else
            {
                await _favorite.BatchDeleteFavoritesAsync(new List<long>() { RelatedId }.ToArray());
            }
            return RedirectToAction("Product","Favorite");
        }
        [HttpPost("Store")]
        public async Task<IActionResult> StoreFavorite(long RelatedId, long? Id)
        {
            var existingRecord = await _favorite.GetFavoriteAsync(new Domain.Requests.Favorites.GetFavoriteRequest()
            {
                Id = (Id ?? 0)
            });
            if (existingRecord == null)
            {
                await _favorite.AddFavoriteAsync(new Domain.Requests.Favorites.AddFavoriteRequest()
                {
                    TypeId = (byte)Domain.Entities.FavoriteType.Store,
                    RelatedId = RelatedId
                });
            }
            else
            {
                await _favorite.BatchDeleteFavoritesAsync(new List<long>() { RelatedId }.ToArray());
            }
            return RedirectToAction("Store", "Favorite");
        }
    }
}
