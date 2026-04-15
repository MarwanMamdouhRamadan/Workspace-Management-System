using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.DTOs.response;

namespace Workspace.Application.Interfaces
{
    public interface IInvoiceServices
    {
        // Customer
        Task<bool> CreateInvoice(CreateInvoiceDto dto);
        Task<InvoiceResponseDto> GetById(long id);
        Task<bool> CancelInvoice(long invoiceId);

        // Admin
        Task<IEnumerable<InvoiceResponseDto>> GetAll();
        Task<IEnumerable<InvoiceResponseDto>> GetAllByUser(string userId);
        Task<bool> MarkAsPaid(long invoiceId);
    }
}
