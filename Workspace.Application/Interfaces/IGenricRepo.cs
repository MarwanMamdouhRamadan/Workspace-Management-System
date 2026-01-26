using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Infrastructure.Repositories.Interfaces
{
    public interface IGenricRepo<T> where T : class
    {
        Task<IEnumerable<T>> getAll();
        Task<T> getById(long id);
        Task add(T entity);
        void update(T entity);
        void delete(T entity);
    }
}
