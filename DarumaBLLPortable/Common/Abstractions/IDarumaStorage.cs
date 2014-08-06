using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common.Abstractions
{
    public interface IDarumaStorage
    {
        Task<bool> Add(DarumaDomain daruma);
        Task<DarumaDomain> GetById(Guid id);
        Task<IEnumerable<DarumaDomain>> ListByIds(IEnumerable<Guid> ids);
        Task<IEnumerable<DarumaDomain>> ListAll();
        Task<bool> Update(DarumaDomain daruma);
        Task<bool> Delete(Guid id);
    }
}
