using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Infrastructure.AppSettingConfigs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.ViewComponents
{
    public class Search : ViewComponent
    {
        #region Lazy loading
        private readonly IShoppingCenterTypeRepository typeRepository;
        private readonly ICityRepository cityRepository;
        private readonly Files _filesConfig = null;


        public Search(IShoppingCenterTypeRepository typeRepository, ICityRepository cityRepository,
            IOptions<Files> filesConfig)
        {
            _filesConfig = filesConfig?.Value;
            this.typeRepository = typeRepository;
            this.cityRepository = cityRepository;
        }
        #endregion

        #region View Models
        public class SearchViewModel
        {
            public List<TypeForsearchModel> typeList { get; set; } = new List<TypeForsearchModel>();

            public List<City> cityList { get; set; } = new List<City>();
        }

        public class TypeForsearchModel
        {
            string _basePath;
            public TypeForsearchModel(string basePath)
            {
                _basePath = basePath;
            }
            public int? TypeId { get; set; }

            public string Name { get; set; }

            public string ImagePath
            {
                get
                {
                    return string.Format("{0}/Sh_C_Types/100_100/{1}.png", this._basePath, this.TypeId);
                }
            }
        }

        #endregion

        #region main methods
        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {

            var model = new SearchViewModel();

            var typeLists = typeRepository
                .List(x=> !x.IsDeleted).ToList();

            foreach (var item in typeLists)
            {
                var type = new TypeForsearchModel(_filesConfig.BaseUrl);
                type.TypeId = item.Id;
                type.Name = item.Name;

                model.typeList.Add(type);
            }


            model.cityList = cityRepository.List(x => !x.IsDeleted).ToList();


            return View(model);
        }
        #endregion

    }
}
