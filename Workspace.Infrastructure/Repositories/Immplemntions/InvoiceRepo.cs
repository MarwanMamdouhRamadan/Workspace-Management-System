using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Data;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class InvoiceRepo : GenricRepo<TbInvoice>, IInvoiceRepo
    {
        public InvoiceRepo(WorkSpaceSysContext db) : base(db)
        {
        }

        public async Task<TbInvoice> GetByIdWithDetails(long id)
        {
            return await _dbSet
                .Include(x => x.TbInvoiceBookings)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TbInvoice>> GetAllWithDetails()
        {
            return await _dbSet
                .Include(x => x.TbInvoiceBookings)
                .Include(x => x.Status)
                .ToListAsync();
        }
    }
}
