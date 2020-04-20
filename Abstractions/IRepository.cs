using BecauseImClever.HomeAutomation.AutomationModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        ValueTask<T> CreateAsync(T entity);
        ValueTask<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);

        ValueTask<IEnumerable<T>> GetAllAsync();
        ValueTask<T> GetByIdAsync(Guid id);

        ValueTask<bool> DeleteAsync(Guid id);
        ValueTask<(bool, long)> DeleteManyAsync(IEnumerable<Guid> ids);
    }
}
