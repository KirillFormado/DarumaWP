using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLL.Domain;

namespace DarumaBLL.Common.Abstractions
{
    public interface IDarumaStorage
    {
        void Add(DarumaDomain daruma);
        DarumaDomain GetById(Guid id);
        IEnumerable<DarumaDomain> GetByStatus(DarumaStatus status);
        Task<IEnumerable<DarumaDomain>> ListAll();
        void Update(DarumaDomain daruma);
        void Delete(Guid id);
    }
}
