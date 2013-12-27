using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using DarumaBLL.Common.Abstractions;
using DarumaBLL.Domain;
using Newtonsoft.Json;

namespace Daruma.Infrastructure.Storages
{
    public class DarumaStorage : IDarumaStorage
    {
        private const string FolderName = "Darumas";

        public async Task<bool> Add(DarumaDomain daruma)
        {
            var darumaJSON = await JsonConvert.SerializeObjectAsync(daruma);
            
            var folder = await GetFolderAsync();
            var file = await folder.CreateFileAsync(daruma.Id.ToString(), CreationCollisionOption.FailIfExists);
            return await WriteFile(file, darumaJSON);
        }

        private async Task<bool> WriteFile(StorageFile file, string darumaJSON)
        {
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                byte[] content = Encoding.UTF8.GetBytes(darumaJSON);
                await stream.WriteAsync(content, 0, content.Length);
                return true;
            }
        }

        public async Task<DarumaDomain> GetById(Guid id)
        {           
            var folder = await GetFolderAsync();
            var file = await folder.GetFileAsync(id.ToString());
            return await DeserializeObject(file);
        }

        private async Task<StorageFolder> GetFolderAsync()
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName,
                CreationCollisionOption.OpenIfExists);
            return folder;
        }

        public IEnumerable<DarumaDomain> GetByStatus(DarumaStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DarumaDomain>> ListAll()
        {
            var darumaList = new List<DarumaDomain>();

            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName,
               CreationCollisionOption.OpenIfExists);

            var files = await folder.GetFilesAsync();

            foreach (var storageFile in files)
            {
                DarumaDomain objDaruma = await DeserializeObject(storageFile);
                darumaList.Add(objDaruma);
            }

            //May by need move order logic to the service 
            return darumaList.OrderBy(d => d.CreateDate);
        }

        public async Task<bool> Update(DarumaDomain daruma)
        {
            var darumaJSON = await JsonConvert.SerializeObjectAsync(daruma);

            var folder = await GetFolderAsync();
            if (folder != null)
            {
                var file = await folder.GetFileAsync(daruma.Id.ToString());
                if (file != null)
                {
                    return await WriteFile(file, darumaJSON);
                }
            }

            return false;
        }

        private async Task<DarumaDomain> DeserializeObject(StorageFile storageFile)
        {
           DarumaDomain objDaruma;
            IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
            using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
            {
                byte[] content = new byte[stream.Length];
                await stream.ReadAsync(content, 0, (int)stream.Length);
                var text = Encoding.UTF8.GetString(content, 0, content.Length);
                objDaruma = await JsonConvert.DeserializeObjectAsync<DarumaDomain>(text);
            }

            return objDaruma;
        }

        public async Task<bool> Delete(Guid id)
        {
            var folder = await GetFolderAsync();
            if (folder != null)
            {
                var file = await folder.GetFileAsync(id.ToString());
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.Default);
                    return true;
                }
            }
            return false;
        }
    }
}
