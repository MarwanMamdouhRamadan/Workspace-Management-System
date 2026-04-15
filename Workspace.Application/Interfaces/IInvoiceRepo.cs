using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IInvoiceRepo : IGenricRepo<TbInvoice>
    {
        Task<TbInvoice> GetByIdWithDetails(long id);
        Task<IEnumerable<TbInvoice>> GetAllWithDetails();
    }
}
