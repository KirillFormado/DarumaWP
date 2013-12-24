using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLL.Domain;

namespace DarumaBLL.Common.Abstractions
{
    public interface IDarumaStorage
    {
        Task<bool> Add(DarumaDomain daruma);
        Task<DarumaDomain> GetById(Guid id);
        IEnumerable<DarumaDomain> GetByStatus(DarumaStatus status);
        Task<IEnumerable<DarumaDomain>> ListAll();
        void Update(DarumaDomain daruma);
        Task<bool> Delete(Guid id);
    }
}
