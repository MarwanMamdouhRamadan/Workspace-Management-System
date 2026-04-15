using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;

namespace Workspace.Application.Interfaces
{
    public interface IBookingServices
    {
        public Task<bool> CreateBooking(BookingDto dto);
        public Task<bool> confirmBooking(long bookingId);
        public Task<bool> completeBooking(long bookingId);
        public Task<bool> cancelBooking(long bookingId);
        public Task<bool> addProductToBooking(BookingProductDto dto);
    }
}
