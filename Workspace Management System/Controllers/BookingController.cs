using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Common;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        IBookingServices bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            this.bookingServices = bookingServices;
        }

        [HttpPost]
        public async Task<IActionResult> createBooking(BookingDto dto)
        {
            await bookingServices.CreateBooking(dto);
            return ApiResponseHelper.Success(Data: "The Booking is created",StatusCode:200);
        }
        [HttpPost("AddProductToBooking")]
        public async Task<IActionResult> addProductToBooking(BookingProductDto dto)
        {
            await bookingServices.addProductToBooking(dto);
            return ApiResponseHelper.Success(Data: "The Product is added to booking", StatusCode: 200);
        }
        [HttpPut("ConfirmBooking/{bookingId}")]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> confirmBooking(long bookingId)
        {
            await bookingServices.confirmBooking(bookingId);
            return ApiResponseHelper.Success(Data: "The Booking is confirmed", StatusCode: 200);
        }
        [HttpPut("CancelBooking/{bookingId}")]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> CancelBooking(long bookingId)
        {
            await bookingServices.cancelBooking(bookingId);
            return ApiResponseHelper.Success(Data: "The Booking is canceld", StatusCode: 200);
        }
        [HttpPut("CompleteBooking/{bookingId}")]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> completeBooking(long bookingId)
        {
            await bookingServices.completeBooking(bookingId);
            return ApiResponseHelper.Success(Data: "The Booking is completed", StatusCode: 200);
        }
    }
}
