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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;

        public InvoiceController(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        [HttpPost]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> createInvoice([FromBody] CreateInvoiceDto dto)
        {
            await _invoiceServices.CreateInvoice(dto);
            return ApiResponseHelper.Success(Data: "The Invoice is created", StatusCode: 200);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(long id)
        {
            var invoice = await _invoiceServices.GetById(id);
            return ApiResponseHelper.Success(Data: invoice, StatusCode: 200);
        }

        [HttpPut("CancelInvoice/{id}")]
        public async Task<IActionResult> cancelInvoice(long id)
        {
            await _invoiceServices.CancelInvoice(id);
            return ApiResponseHelper.Success(Data: "The Invoice is cancelled", StatusCode: 200);
        }


        [HttpGet]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> getAll()
        {
            var invoices = await _invoiceServices.GetAll();
            return ApiResponseHelper.Success(invoices, StatusCode: 200);
        }

        [HttpGet("GetByUser/{userId}")]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> getByUser(string userId)
        {
            var invoices = await _invoiceServices.GetAllByUser(userId);
            return ApiResponseHelper.Success(invoices, StatusCode: 200);
        }

        [HttpPut("MarkAsPaid/{id}")]
        [Authorize(Roles = SystemConstants.Roles.Admin)]
        public async Task<IActionResult> markAsPaid(long id)
        {
            await _invoiceServices.MarkAsPaid(id);
            return ApiResponseHelper.Success(Data: "The Invoice is marked as paid", StatusCode: 200);
        }
    }
}
