using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Domain.Entities;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoomRepo roomRepo { get; }
        public IBookingRepo bookingRepo { get; }
        public IProductRepo productRepo { get; }
        public IRoomRateRepo roomRateRepo { get; }
        IGenricRepo<TbStatus> statusRepo { get; }
        IGenricRepo<TbStatusType> statusTypeRepo { get; }
        IGenricRepo<TbBookingProduct> bookingProductRepo {  get; }
        IInvoiceRepo invoiceRepo { get; }
        IGenricRepo<TbInvoiceBooking> invoiceBookingRepo { get; }
        Task<int> CompleteAsync();
    }
}
