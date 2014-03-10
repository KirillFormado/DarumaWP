using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using DarumaBLLPortable.Common;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Utils;

namespace DarumaDAL.WP.Storages
{
    public class DarumaStorage : IDarumaStorage
    {
        private const string FolderName = "Darumas";

        public async Task<bool> Add(DarumaDomain daruma)
        {
            var darumaJSON = await SerializeObject(daruma.GetDTO());
            
            var folder = await GetFolderAsync();
            var file = await folder.CreateFileAsync(daruma.Id.ToString(), CreationCollisionOption.FailIfExists);
            return await WriteFile(file, darumaJSON);
        }

        private async Task<bool> WriteFile(StorageFile file, string darumaJSON)
        {
            try
            {
                byte[] content = Encoding.UTF8.GetBytes(darumaJSON);
                Stream stream = await file.OpenStreamForWriteAsync();
                
                await stream.WriteAsync(content, 0, content.Length);

                stream.Flush();
                stream.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
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
            var darumaJSON = await SerializeObject(daruma.GetDTO());
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

        private async Task<string> SerializeObject(DarumaDTO obj)
        {
            var str = await Serializer.Serialize<DarumaDTO>(obj);
            return str;
        }

        private async Task<DarumaDomain> DeserializeObject(StorageFile storageFile)
        {
            DarumaDTO objDaruma;
            IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
            using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
            {
                var content = new byte[stream.Length];
                await stream.ReadAsync(content, 0, (int)stream.Length);
                var text = Encoding.UTF8.GetString(content, 0, content.Length);
                objDaruma = await Serializer.Deserialize<DarumaDTO>(text);
            }

            return new DarumaDomain(objDaruma);
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
