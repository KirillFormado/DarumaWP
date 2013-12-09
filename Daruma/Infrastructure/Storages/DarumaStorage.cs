using System;
using System.Collections.Generic;
using System.IO;
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

        public async void Add(DarumaDomain daruma)
        {
            var darumaJSON = await JsonConvert.SerializeObjectAsync(daruma);


            var folder = await GetFolderAsync();
            var file = await folder.CreateFileAsync(daruma.Id.ToString(), CreationCollisionOption.FailIfExists);
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                byte[] content = Encoding.UTF8.GetBytes(darumaJSON);
                stream.WriteAsync(content, 0, content.Length);
            }
        }

        public DarumaDomain GetById(Guid id)
        {
            return null;
            //var folder = await GetFolderAsync();
            //var file = await folder.GetFileAsync(id.ToString());
            //return await DeserializeObject(file);
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

            return darumaList;
        }

        public void Update(DarumaDomain daruma)
        {
            throw new NotImplementedException();
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

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
