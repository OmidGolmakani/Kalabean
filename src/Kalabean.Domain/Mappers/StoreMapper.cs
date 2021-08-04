﻿using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Store;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class StoreMapper : IStoreMapper
    {
        private readonly IFloorMapper _floorMapper;
        private readonly ICategoryMapper _categoryMapper;
        private readonly IShoppingCenterMapper _shoppingCenterMapper;
        private readonly IShoppingCenterTypeMapper _typeMapper;

        public StoreMapper(IFloorMapper floorMapper,
            ICategoryMapper categoryMapper,
            IShoppingCenterMapper shoppingCenterMapper,
            IShoppingCenterTypeMapper typeMapper)
        {
            _floorMapper = floorMapper;
            _categoryMapper = categoryMapper;
            _shoppingCenterMapper = shoppingCenterMapper;
            _typeMapper = typeMapper;
        }

        public Store Map(AddStoreRequest request)
        {
            if (request == null) return null;

            var store = new Store
            {
                Name = request.Name,
                //TypeId = request.TypeId,
                ShoppingCenterId = request.ShoppingCenterId,
                CategoryId = request.CategoryId,
                FloorId = request.FloorId,
                Address = request.Address,
                Description = request.Description,
                DiscountPercentage = request.DiscountPercentage,
                DiscountCoupon = request.DiscountCoupon,
                AuctionPercentage = request.AuctionPercentage,
                Tel = request.Tel,
                Mobile = request.Mobile,
                Email = request.Email,
                WorkingHours = request.WorkingHours,
                StoreNo = request.StoreNo,
                HasImage = request.Image != null && request.Image.Length > 0,
                VirtualTourUrl = request.VirtualTourUrl,
                InstagramUrl = request.InstagramUrl,
                TelegramUrl = request.TelegramUrl,
                WebsiteUrl = request.WebsiteUrl,
                IsEnabled = request.IsEnabled,
                Lat = request.Lat,
                Lng = request.Lng
            };
            return store;
        }

        public Store Map(EditStoreRequest request)
        {
            if (request == null) return null;

            var store = new Store
            {
                Name = request.Name,
                //TypeId = request.TypeId,
                ShoppingCenterId = request.ShoppingCenterId,
                CategoryId = request.CategoryId,
                FloorId = request.FloorId,
                Address = request.Address,
                Description = request.Description,
                DiscountPercentage = request.DiscountPercentage,
                DiscountCoupon = request.DiscountCoupon,
                AuctionPercentage = request.AuctionPercentage,
                Tel = request.Tel,
                Mobile = request.Mobile,
                Email = request.Email,
                WorkingHours = request.WorkingHours,
                StoreNo = request.StoreNo,
                VirtualTourUrl = request.VirtualTourUrl,
                InstagramUrl = request.InstagramUrl,
                TelegramUrl = request.TelegramUrl,
                WebsiteUrl = request.WebsiteUrl,
                IsEnabled = request.IsEnabled,
                Lat = request.Lat,
                Lng = request.Lng
            };
            if (request.ImageEdited)
            {
                store.HasImage = request.Image != null && request.Image.Length > 0;
            }

            return store;
        }
        public StoreResponse Map(Store store)
        {
            if (store == null) return null;

            var response = new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                //Type = _typeMapper.Map(store.Type),
                ShoppingCenter = _shoppingCenterMapper.Map(store.ShoppingCenter),
                Category = _categoryMapper.Map(store.Category),
                Floor = _floorMapper.Map(store.Floor),
                Address = store.Address,
                Description = store.Description,
                DiscountPercentage = store.DiscountPercentage,
                DiscountCoupon = store.DiscountCoupon,
                AuctionPercentage = store.AuctionPercentage,
                Tel = store.Tel,
                Mobile = store.Mobile,
                Email = store.Email,
                WorkingHours = store.WorkingHours,
                StoreNo = store.StoreNo,
                VirtualTourUrl = store.VirtualTourUrl,
                InstagramUrl = store.InstagramUrl,
                TelegramUrl = store.TelegramUrl,
                WebsiteUrl = store.WebsiteUrl,
                IsEnabled = store.IsEnabled,
                Lat = store.Lat,
                Lng = store.Lng
            };
            if (store.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Stores/{store.Id}.jpeg";
            return response;
        }
    }
}
