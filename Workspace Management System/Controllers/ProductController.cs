using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;
using Workspace.Infrastructure;
using Workspace_Management_System.Entities;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetProductByStatus/{statusId}")]
        public async Task<IActionResult> getProductsByStatus(long statusId)
        {
            var products = await _productServices.getProductsByStatus(statusId);
            return ApiResponseHelper.Success(products, StatusCode: 200);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var products = await _productServices.getAll();
            return ApiResponseHelper.Success(products, StatusCode: 200);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(long id)
        {
            var product = await _productServices.getById(id);
            return ApiResponseHelper.Success(Data: product, StatusCode: 200);
        }
        [HttpPost]
        public async Task<IActionResult> addProduct([FromBody] ProductDto dto)
        {
            await _productServices.addProduct(dto);
            return ApiResponseHelper.Success(Data: "Product is craeted", StatusCode: 200);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putProduct(long id, [FromBody] ProductDto dto)
        {
            await _productServices.putProduct(id, dto);
            return ApiResponseHelper.Success(Data: "Product is updated", StatusCode: 200);
        }
        [HttpPut("DeleteProduct/{id}")]
        public async Task<IActionResult> deleteProduct(long id)
        {
             await _productServices.deleteProduct(id);
             return ApiResponseHelper.Success(Data: "Product is deleted", StatusCode: 200);
        }
        [HttpPut("ChangeProductStatus")]
        public async Task<IActionResult> changeProductStatus([FromBody]PutProductStatus dto)
        {
            await _productServices.changeProductStatus(dto);
            return ApiResponseHelper.Success(Data: "Product is updated", StatusCode: 200);
        }
    }
}
