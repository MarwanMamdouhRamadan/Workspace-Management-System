using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Data;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkSpaceSysContext _context;

        public UnitOfWork(WorkSpaceSysContext context, IRoomRepo roomRepo, IBookingRepo bookingRepo, IProductRepo productRepo, IRoomRateRepo roomRateRepo, IGenricRepo<TbStatus> statusRepo, IGenricRepo<TbStatusType> statusTypeRepo, IGenricRepo<TbBookingProduct> bookingProductRepo, IInvoiceRepo invoiceRepo, IGenricRepo<TbInvoiceBooking> invoiceBookingRepo)
        {
            _context = context;
            this.roomRepo = roomRepo;
            this.bookingRepo = bookingRepo;
            this.productRepo = productRepo;
            this.roomRateRepo = roomRateRepo;
            this.statusRepo = statusRepo;
            this.statusTypeRepo = statusTypeRepo;
            this.bookingProductRepo = bookingProductRepo;
            this.invoiceRepo = invoiceRepo;
            this.invoiceBookingRepo = invoiceBookingRepo;
        }
        public IRoomRepo roomRepo { get; }

        public IBookingRepo bookingRepo { get; }

        public IProductRepo productRepo { get; }

        public IRoomRateRepo roomRateRepo { get; }

        public IGenricRepo<TbStatus> statusRepo { get; }

        public IGenricRepo<TbStatusType> statusTypeRepo { get; }
        public IGenricRepo<TbBookingProduct> bookingProductRepo { get; }

        public IInvoiceRepo invoiceRepo { get; }

        public IGenricRepo<TbInvoiceBooking> invoiceBookingRepo { get; }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
