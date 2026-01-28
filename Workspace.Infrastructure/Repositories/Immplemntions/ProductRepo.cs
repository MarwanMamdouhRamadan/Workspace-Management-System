using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Common;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Data;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class ProductRepo : GenricRepo<TbProduct>, IProductRepo
    {
        public ProductRepo(WorkSpaceSysContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TbProduct>> getAllActivatedProducts()
        {
            return await _dbSet.Include(p => p.tbStatus).Where(x => x.tbStatus.StatusName == SystemConstants.ProductStatus.Active).ToListAsync();
        }

        public async Task<IEnumerable<TbProduct>> getAllProducts(long statusId)
        {
            return await _dbSet.Where(x => x.StatusId == statusId).ToListAsync();
        }

        public async Task<TbProduct> getProductById(long id)
        {
            return await _dbSet.Include(p => p.tbStatus)
                .Where(x => x.tbStatus.StatusName == SystemConstants.ProductStatus.Active).FirstOrDefaultAsync(x=> x.Id == id);
        }
    }
}
