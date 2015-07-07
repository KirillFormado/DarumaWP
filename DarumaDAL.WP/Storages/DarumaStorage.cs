using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Common;
using DarumaDAL.WP.EntityMappers;

namespace DarumaDAL.WP.Storages
{
    public class DarumaStorage : StorageBase<DarumaDomain, DarumaDTO>, IDarumaStorage
    {
        protected override string FolderName { get { return "Darumas"; } }

        private DarumaMapper _mapper = new DarumaMapper();
        protected override IMapper<DarumaDomain, DarumaDTO> Mapper
        {
            get
            {
                return _mapper;
            }            
        }

        public async Task<bool> Add(DarumaDomain daruma)
        {
            return await Add(daruma, daruma.Id.ToString());            
        }        

        public async Task<DarumaDomain> GetById(Guid id)
        {
            return await GetById(id.ToString());           
        }

        public async Task<IEnumerable<DarumaDomain>> ListByIds(IEnumerable<Guid> ids)
        {
            return await ListByIds(ids.Select(id => id.ToString()));
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
