using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Infrastructure.Repositories.Interfaces;
using Workspace_Management_System.Data;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class GenricRepo<T> : IGenricRepo<T> where T : class
    {
        protected readonly WorkSpaceSysContext db;
        protected readonly DbSet<T> _dbSet;

        public GenricRepo(WorkSpaceSysContext db)
        {
            this.db = db;
            _dbSet = db.Set<T>();
        }

        public async Task add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public void delete(T entity)
        {
            _dbSet.Remove(entity);
            db.SaveChanges();
        }

        public async Task<IEnumerable<T>> getAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> getById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void update(T entity)
        {
            _dbSet.Update(entity);
            db.SaveChanges();
        }
    }
}
