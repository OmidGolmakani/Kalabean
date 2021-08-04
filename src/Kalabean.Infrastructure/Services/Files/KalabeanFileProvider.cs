using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Kalabean.Infrastructure.Files
{
    public class KalabeanFileProvider
    {
        private const string Image_Base_Path = "KL_ImagesRepo";
        private const string Cities_Sub_Directory = "Cities";
        private const string Types_Sub_Directory = "Sh_C_Types";
        private const string Shoppings_Sub_Directory = "Sh_C";
        private const string Stores_Sub_Directory = "Stores";

        IFileAccessProvider _fileProvider;
        public KalabeanFileProvider(IFileAccessProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public bool SaveCityImage(Stream stream, int cityId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Cities_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{cityId}.jpeg");
            saveStreamAsFile(filePath, stream);
            return true;
        }

        public bool DeleteCityImage(int cityId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Cities_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{cityId}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        public bool SaveTypeImage(Stream stream, int typeId)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Types_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{typeId}.jpeg");
            saveStreamAsFile(filePath, stream);
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

        public bool SaveShoppingCenterImage(Stream stream, int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Shoppings_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return true;
        }

        public bool DeleteShoppingCenterImage(int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Shoppings_Sub_Directory);
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            if (File.Exists(filePath))
                _fileProvider.DeleteFile(filePath);
            return true;
        }

        public bool SaveStoreImage(Stream stream, int id)
        {
            string path = _fileProvider.Combine(Image_Base_Path, Stores_Sub_Directory);
            if (!_fileProvider.DirectoryExists(path))
                _fileProvider.CreateDirectory(path);
            // TODO: What about other extensions like jpg and so on.
            string filePath = _fileProvider.Combine(path, $"{id}.jpeg");
            saveStreamAsFile(filePath, stream);
            return true;
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
    }
}
