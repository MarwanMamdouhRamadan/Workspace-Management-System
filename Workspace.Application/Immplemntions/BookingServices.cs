using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.Common;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class BookingServices : IBookingServices
    {
        IUnitOfWork _unitOfWork;
        IStatusLookupService _lookupService;
        public BookingServices(IUnitOfWork unitOfWork, IStatusLookupService lookupService)
        {
            _unitOfWork = unitOfWork;
            _lookupService = lookupService;
        }

        public async Task<bool> addProductToBooking(BookingProductDto dto)
        {
            TbBooking booking = await _unitOfWork.bookingRepo.getById(dto.BookingId);
            if (booking == null) if (booking == null) throw new KeyNotFoundException("Not Found Booking ");
            long CancelledStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Cancel);
            long CompletedStatusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Cancel);
            if (booking.StatusId == CancelledStatusId || booking.StatusId == CompletedStatusId)
                throw new InvalidOperationException("Cannot add products to a cancelled or completed booking.");
            TbProduct product = await _unitOfWork.productRepo.getById(dto.ProductId);
            if (product == null) throw new KeyNotFoundException("Not found product");
            if (product.Stock < dto.ProductQty)
                throw new InvalidOperationException($"Insufficient stock. Available: {product.Stock}");
            TbBookingProduct bookingProduct = new TbBookingProduct
            {
                BookingId = dto.BookingId,
                ProductId = product.Id,
                ProductQty = dto.ProductQty,
                ProductUnitPrice = product.Price,
                ProductTotalPrice = product.Price * dto.ProductQty
            };
            product.Stock -= dto.ProductQty;
            _unitOfWork.productRepo.update(product);
            await _unitOfWork.bookingProductRepo.add(bookingProduct);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<decimal> CaculatePrice(long roomId ,int CountOfPeople, string Mode, double totalMinutes)
        {
            var roomRate = await _unitOfWork.roomRateRepo.getRoomRate(roomId, Mode);
            if (roomRate == null) throw new KeyNotFoundException("Not found room assigned to rates");
            decimal durationInHours = (decimal)totalMinutes / 60.0m;
            decimal price = durationInHours * roomRate.HourlyRate;
            if(Mode.Equals(SystemConstants.RoomType.Shared ,StringComparison.OrdinalIgnoreCase ) || Mode.Equals(SystemConstants.RoomType.Private_Group, StringComparison.OrdinalIgnoreCase) )
            {
                price *= CountOfPeople;
            }
            return Math.Round(price,2);

        }

        public async Task<bool> cancelBooking(long bookingId)
        {
            TbBooking booking = await _unitOfWork.bookingRepo.getById(bookingId);
            if (booking == null) throw new KeyNotFoundException("Not Found Booking to cancel");
            long statusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Cancel);
            if (statusId == 0) throw new KeyNotFoundException("System Error: 'Cancel' status configuration for booking is missing.");
            booking.StatusId = statusId;
            _unitOfWork.bookingRepo.update(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> checkTimeAvablity(DateTime start , DateTime end,string mode, long roomId, int countOfPeople)
        {
            if (start.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new ArgumentException("Start time must be in the future.");
            }

            if ((end - start).TotalMinutes < 30)
            {
                throw new ArgumentException("The minimum booking duration is 30 minutes.");
            }
            bool isReserved = await _unitOfWork.bookingRepo.isReserved(start, end ,mode,roomId,countOfPeople);
            
            return !isReserved;
        }

        public async Task<bool> completeBooking(long bookingId)
        {
            TbBooking booking = await _unitOfWork.bookingRepo.getById(bookingId);
            if (booking == null) throw new KeyNotFoundException("Not Found Booking to cancel");
            long statusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Complete);
            if (statusId == 0) throw new KeyNotFoundException("System Error: 'Complete' status configuration for booking is missing.");
            booking.StatusId = statusId;
            _unitOfWork.bookingRepo.update(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> confirmBooking(long bookingId)
        {
            TbBooking booking = await _unitOfWork.bookingRepo.getById(bookingId);
            if (booking == null) throw new KeyNotFoundException("Not Found Booking to cancel");
            long statusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Confirmed);
            if (statusId == 0) throw new KeyNotFoundException("System Error: 'Confirmed' status configuration for booking is missing.");
            booking.StatusId = statusId;
            _unitOfWork.bookingRepo.update(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> CreateBooking(BookingDto dto)
        {
            
            bool timeAvablity = await checkTimeAvablity(dto.StartTime,dto.EndTime,dto.Mode,dto.RoomId,dto.CountOfPeople);
            if (timeAvablity == false) throw new InvalidOperationException("This time slot is already reserved or count of people is greater than capcity.");
            long statusId = _lookupService.GetStatusId(SystemConstants.StatusTypes.Booking, SystemConstants.Booking.Pending);
            if (statusId == 0)
                throw new KeyNotFoundException("System Error: 'Pending' status configuration for booking is missing.");
            TimeSpan duaration = (dto.EndTime - dto.StartTime);
            double totalMinutes = duaration.TotalMinutes;
            decimal roomPrice = await CaculatePrice(dto.RoomId,dto.CountOfPeople,dto.Mode,totalMinutes);
            TbBooking booking = new TbBooking
            {
                CountOfPeople = dto.CountOfPeople,
                StartTime  = dto.StartTime,
                EndTime = dto.EndTime,
                CreatedDate = DateTime.UtcNow,
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                StatusId = statusId,
                RoomPrice = roomPrice,
            };
            await _unitOfWork.bookingRepo.add(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
