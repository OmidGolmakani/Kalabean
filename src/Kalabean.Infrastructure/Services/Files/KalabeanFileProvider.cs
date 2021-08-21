using Kalabean.Domain.Requests.ResizeImage;
using Kalabean.Infrastructure.Services.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Drawing;
using System.IO;

namespace Kalabean.Infrastructure.Files
{
    public class KalabeanFileProvider
    {
        private const string Image_Base_Path = "KL_ImagesRepo";
        private const string Cities_Sub_Directory = "Cities";
        private const string Types_Sub_Directory = "Sh_C_Types";
        private const string Shoppings_Sub_Directory = "Sh_C";
        private const string Possibilities_Shoppings_Sub_Directory = "Possibilities_Shoppings";
        private const string Stores_Sub_Directory = "Stores";
        private const string Products_Sub_Directory = "Products";
        private const string Articles_Sub_Directory = "Articles";
        private const string Advertising_Sub_Directory = "Advertising";
        private const string Orders_Sub_Directory = "Orders";
        private const string File_Base_Path = @"KL_ImagesRepo\Files";


        private IFileAccessProvider _fileProvider;
        private IResizeImageService<long> _imageService;

        public KalabeanFileProvider(IFileAccessProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public Tuple<bool, string> SaveCityImage(Stream stream, int cityId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Cities_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{cityId}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public Tuple<bool, string> SaveAdvertiseImage(Stream stream, int AdsId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Advertising_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{AdsId}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public Tuple<bool, string>  SaveTypeImage(Stream stream, int typeId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Types_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{typeId}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public bool DeleteCityImage(int cityId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Cities_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{cityId}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public bool DeleteAdvertiseImage(int AdsId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Advertising_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{AdsId}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        public bool DeleteTypeImage(int typeId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Types_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{typeId}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        public Tuple<bool, string> SaveShoppingCenterImage(Stream stream, int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Shoppings_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public Tuple<bool, string> SavePossibilitiesShopImage(Stream stream, int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Possibilities_Shoppings_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public bool DeleteShoppingCenterImage(int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Shoppings_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public bool DeletePossibilitiesShoppingCenterImage(int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Possibilities_Shoppings_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        public Tuple<bool, string> SaveStoreImage(Stream stream, int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Stores_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }

        public bool DeleteStoreImage(int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Stores_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        private void saveStreamAsFile(string filename, Stream fileStream)
        {
            using (FileStream outputFileStream = new FileStream(filename, FileMode.Create))
            {
                fileStream.CopyTo(outputFileStream);
            }
        }
        public Tuple<bool, string> SaveArticleImage(Stream stream, long id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Articles_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public bool DeleteArticleImage(long id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Articles_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public Tuple<bool, string> SaveProductImage(Stream stream, long id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Products_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public bool DeleteProductImage(int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Products_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public bool SaveProductFile(Stream stream, long id, string extention)
        {
            string path = _fileProvider.Combine(File_Base_Path, Products_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);

            string filePath = _fileProvider.Combine(path, $"{id}{extention}");
            saveStreamAsFile(filePath, stream);
            return true;
        }

        public bool SaveArticleFile(Stream stream, long id, string extention)
        {
            string path = _fileProvider.Combine(File_Base_Path, Articles_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}{extention}");
            saveStreamAsFile(filePath, stream);
            return true;
        }
        public bool DeleteArticleFile(long id, string extention)
        {
            string path = _fileProvider.Combine(File_Base_Path, Articles_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}{extention}");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public bool DeleteProductFile(long id, string extention)
        {
            string path = _fileProvider.Combine(File_Base_Path, Products_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}{extention}");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
        public Tuple<bool, string> SaveOrderImage(Stream stream, long OrderId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Orders_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{OrderId}.jpeg");
            saveStreamAsFile(filePath, stream);
            return new Tuple<bool, string>(true, filePath);
        }
        public bool DeleteOrderImage(long OrderId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Orders_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{OrderId}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }
    }
}
