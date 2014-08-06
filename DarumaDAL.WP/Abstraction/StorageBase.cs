using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using DarumaDAL.WP.Infrastructure;

namespace DarumaDAL.WP.Abstraction
{
    public abstract class StorageBase<Domain, DTO> where Domain : class
    {
        protected abstract string FolderName { get; }
        protected abstract IMapper<Domain, DTO> Mapper { get; }

        public async Task<bool> Add(Domain domainObject, string id)
        {
            var json = await SerializeObject(domainObject);
            
            var folder = await GetFolderAsync();
            var file = await folder.CreateFileAsync(id, CreationCollisionOption.FailIfExists);
            return await WriteFile(file, json);
        }

        private async Task<bool> WriteFile(StorageFile file, string json)
        {
            try
            {
                byte[] content = Encoding.UTF8.GetBytes(json);
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

        public async Task<Domain> GetById(string id)
        {           
            var folder = await GetFolderAsync();
            var file = await folder.GetFileAsync(id);
            return await DeserializeObject(file);
        }

        private async Task<StorageFolder> GetFolderAsync()
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName,
                CreationCollisionOption.OpenIfExists);
            return folder;
        }        

        public async virtual Task<IEnumerable<Domain>> ListAll()
        {
            var domainList = new List<Domain>();

            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderName,
               CreationCollisionOption.OpenIfExists);

            var files = await folder.GetFilesAsync();

            foreach (var storageFile in files)
            {
                Domain domainObj = await DeserializeObject(storageFile);
                domainList.Add(domainObj);
            }
                     
            return domainList;
        }

        public async Task<bool> Update(Domain domainObject, string id)
        {
            var json = await SerializeObject(domainObject);
            var folder = await GetFolderAsync();
            if (folder != null)
            {
                var file = await folder.GetFileAsync(id);
                if (file != null)
                {
                    return await WriteFile(file, json);
                }
            }

            return false;
        }

        private async Task<string> SerializeObject(Domain obj)
        {
            DTO dto = Mapper.MapToDTO(obj);
            string str = await Serializer.Serialize<DTO>(dto);
            return str;
        }

        private async Task<Domain> DeserializeObject(StorageFile storageFile)
        {
            DTO dto;
            IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
            using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
            {
                var content = new byte[stream.Length];
                await stream.ReadAsync(content, 0, (int)stream.Length);
                var text = Encoding.UTF8.GetString(content, 0, content.Length);
                dto = await Serializer.Deserialize<DTO>(text);
            }

            Domain domain = Mapper.MapToDomain(dto);
            return domain;
        }

        public async Task<bool> Delete(string id)
        {
            var folder = await GetFolderAsync();
            if (folder != null)
            {
                var file = await folder.GetFileAsync(id);
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
