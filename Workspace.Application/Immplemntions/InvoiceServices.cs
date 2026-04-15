using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.Common;
using Workspace.Application.DTOs.request;
using Workspace.Application.DTOs.response;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Immplemntions
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatusLookupService _lookupService;

        public InvoiceServices(IUnitOfWork unitOfWork, IStatusLookupService lookupService)
        {
            _unitOfWork = unitOfWork;
            _lookupService = lookupService;
        }

        public async Task<bool> CreateInvoice(CreateInvoiceDto dto)
        {
            if (dto.BookingIds == null || !dto.BookingIds.Any())
                throw new ArgumentException("At least one booking ID must be provided.");

            long pendingStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Invoice, SystemConstants.InvoiceStatus.Pending);
            if (pendingStatusId == 0)
                throw new KeyNotFoundException("System Error: 'Pending' status configuration for invoices is missing.");

            decimal totalRoomPrice = 0;
            decimal totalProductPrice = 0;
            var invoiceBookings = new List<TbInvoiceBooking>();

            foreach (var bookingId in dto.BookingIds)
            {
                TbBooking booking = await _unitOfWork.bookingRepo.getById(bookingId);
                if (booking == null)
                    throw new KeyNotFoundException($"Booking with ID {bookingId} was not found.");

                if (booking.UserId != dto.UserId)
                    throw new InvalidOperationException($"Booking {bookingId} does not belong to the specified user.");

                long completedStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Complete);
                if (booking.StatusId != completedStatusId)
                    throw new InvalidOperationException($"Booking {bookingId} must be completed before generating an invoice.");

                decimal bookingProductTotal = booking.TbBookingProducts
                    .Sum(p => p.ProductTotalPrice);

                totalRoomPrice += booking.RoomPrice;
                totalProductPrice += bookingProductTotal;

                invoiceBookings.Add(new TbInvoiceBooking
                {
                    BookingId = bookingId,
                    BookingRoomPrice = booking.RoomPrice,
                    BookingProductPrice = bookingProductTotal,
                    BookingTotal = booking.RoomPrice + bookingProductTotal
                });
            }

            decimal subTotal = totalRoomPrice + totalProductPrice;
            decimal discountAmount = Math.Round(subTotal * (dto.DiscountPercentage / 100), 2);
            decimal totalAfterDiscount = subTotal - discountAmount;
            decimal taxAmount = Math.Round(totalAfterDiscount * (dto.TaxPercentage / 100), 2);
            decimal grandTotal = totalAfterDiscount + taxAmount;

            TbInvoice invoice = new TbInvoice
            {
                InvoiceNumber = GenerateInvoiceNumber(),
                UserId = dto.UserId,
                CreatedBy = dto.CreatedBy,
                UpdtedBy = dto.CreatedBy,
                InvoiceDate = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalRoomPrice = totalRoomPrice,
                TotalProductPrice = totalProductPrice,
                SubTotal = subTotal,
                DiscountPercentage = dto.DiscountPercentage,
                DiscountAmount = discountAmount,
                TotalAfterDiscount = totalAfterDiscount,
                TaxPercentage = dto.TaxPercentage,
                TaxAmount = taxAmount,
                GrandTotal = grandTotal,
                StatusId = pendingStatusId,
                Notes = dto.Notes,
            };

            await _unitOfWork.invoiceRepo.add(invoice);
            await _unitOfWork.CompleteAsync();

            foreach (var ib in invoiceBookings)
            {
                ib.InvoiceId = invoice.Id;
                await _unitOfWork.invoiceBookingRepo.add(ib);
            }

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> CancelInvoice(long invoiceId)
        {
            TbInvoice invoice = await _unitOfWork.invoiceRepo.getById(invoiceId);
            if (invoice == null)
                throw new KeyNotFoundException($"Invoice with ID {invoiceId} was not found.");

            long cancelledStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Invoice, SystemConstants.InvoiceStatus.Cancelled);
            if (cancelledStatusId == 0)
                throw new KeyNotFoundException("System Error: 'Cancelled' status configuration for invoices is missing.");

            long paidStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Invoice, SystemConstants.InvoiceStatus.Paid);
            if (invoice.StatusId == paidStatusId)
                throw new InvalidOperationException("Cannot cancel an invoice that has already been paid.");

            invoice.StatusId = cancelledStatusId;
            invoice.UpdtedBy = "System";
            invoice.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.invoiceRepo.update(invoice);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> MarkAsPaid(long invoiceId)
        {
            TbInvoice invoice = await _unitOfWork.invoiceRepo.getById(invoiceId);
            if (invoice == null)
                throw new KeyNotFoundException($"Invoice with ID {invoiceId} was not found.");

            long paidStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Invoice, SystemConstants.InvoiceStatus.Paid);
            if (paidStatusId == 0)
                throw new KeyNotFoundException("System Error: 'Paid' status configuration for invoices is missing.");

            long cancelledStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Invoice, SystemConstants.InvoiceStatus.Cancelled);
            if (invoice.StatusId == cancelledStatusId)
                throw new InvalidOperationException("Cannot mark a cancelled invoice as paid.");

            if (invoice.StatusId == paidStatusId)
                throw new InvalidOperationException("Invoice is already marked as paid.");

            invoice.StatusId = paidStatusId;
            invoice.UpdtedBy = "System";
            invoice.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.invoiceRepo.update(invoice);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetAll()
        {
            var invoices = await _unitOfWork.invoiceRepo.GetAllWithDetails();
            if (invoices == null || !invoices.Any())
                return Enumerable.Empty<InvoiceResponseDto>();

            return invoices.Select(MapToResponseDto).ToList();
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetAllByUser(string userId)
        {
            var invoices = await _unitOfWork.invoiceRepo.GetAllWithDetails();
            if (invoices == null || !invoices.Any())
                return Enumerable.Empty<InvoiceResponseDto>();

            return invoices
                .Where(x => x.UserId == userId)
                .Select(MapToResponseDto)
                .ToList();
        }

        public async Task<InvoiceResponseDto> GetById(long id)
        {
            TbInvoice invoice = await _unitOfWork.invoiceRepo.GetByIdWithDetails(id);
            if (invoice == null)
                throw new KeyNotFoundException($"Invoice with ID {id} was not found.");

            return MapToResponseDto(invoice);
        }

        private InvoiceResponseDto MapToResponseDto(TbInvoice invoice)
        {
            var statusName = _lookupService.getStatus(invoice.StatusId, SystemConstants.StatusTypes.Invoice)?.StatusName ?? "Unknown";

            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                UserId = invoice.UserId,
                InvoiceDate = invoice.InvoiceDate,
                TotalRoomPrice = invoice.TotalRoomPrice,
                TotalProductPrice = invoice.TotalProductPrice,
                SubTotal = invoice.SubTotal,
                DiscountPercentage = invoice.DiscountPercentage,
                DiscountAmount = invoice.DiscountAmount,
                TotalAfterDiscount = invoice.TotalAfterDiscount,
                TaxPercentage = invoice.TaxPercentage,
                TaxAmount = invoice.TaxAmount,
                GrandTotal = invoice.GrandTotal,
                Status = statusName,
                Notes = invoice.Notes,
                Bookings = invoice.TbInvoiceBookings.Select(ib => new InvoiceBookingItemDto
                {
                    BookingId = ib.BookingId,
                    BookingRoomPrice = ib.BookingRoomPrice,
                    BookingProductPrice = ib.BookingProductPrice,
                    BookingTotal = ib.BookingTotal
                }).ToList()
            };
        }

        private static string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}
