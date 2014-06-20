using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using DarumaBLLPortable.Common.Abstractions;

namespace DarumaDAL.WP.Storages
{
    public class DarumaStorage : StorageBase<DarumaDomain>, IDarumaStorage
    {
        protected override string FolderName { get { return "Darumas"; } }
       
        public async Task<bool> Add(DarumaDomain daruma)
        {
            return await Add(daruma, daruma.Id.ToString());            
        }        

        public async Task<DarumaDomain> GetById(Guid id)
        {
            return await GetById(id.ToString());
        }       

        public IEnumerable<DarumaDomain> GetByStatus(DarumaStatus status)
        {
            throw new NotImplementedException();
        }       

        public async Task<bool> Update(DarumaDomain daruma)
        {
            return await Update(daruma, daruma.Id.ToString());    
        }        

        public async Task<bool> Delete(Guid id)
        {
            return await Delete(id.ToString());
        }
    }
}
