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
            return products != null ?ApiResponseHelper.Success(products, StatusCode: 200) : ApiResponseHelper.Failure(Errors:"Not found products",StatusCode:404);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var products = await _productServices.getAll();

            return products != null ? ApiResponseHelper.Success(products, StatusCode: 200): ApiResponseHelper.Failure(Errors: "No Products yet", 404) ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(long id)
        {
            var product = await _productServices.getById(id);

            return product != null ? ApiResponseHelper.Success(Data: product, StatusCode: 200) : ApiResponseHelper.Failure(Errors: "Not Found Product", StatusCode: 404);
        }
        [HttpPost]
        public async Task<IActionResult> addProduct([FromBody] ProductDto dto)
        {
            try
            {
                var check = await _productServices.addProduct(dto);
                return check == true? ApiResponseHelper.Success(Data: "Product is craeted", StatusCode: 200) : ApiResponseHelper.Failure(Errors: "Faild to create product ", StatusCode: 200);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Failure(Errors: "Faild to create product ", StatusCode: 500);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putProduct(long id, [FromBody] ProductDto dto)
        {
            try
            {
               var check =  await _productServices.putProduct(id, dto);
                return check == true ? ApiResponseHelper.Success(Data: "Product is updated", StatusCode: 200) : ApiResponseHelper.Failure(Errors: "Faild to update product ", StatusCode: 200);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Failure(Errors: "Faild to update product ", StatusCode: 500);
            }
        }
        [HttpPut("DeleteProduct/{id}")]
        public async Task<IActionResult> deleteProduct(long id)
        {
            try
            {
                var check = await _productServices.deleteProduct(id);
                return check == true ? ApiResponseHelper.Success(Data: "Product is deleted", StatusCode: 200) : ApiResponseHelper.Failure(Errors: "Faild to delete product ", StatusCode: 200);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Failure(Errors: "Faild to delete product ", StatusCode: 500);
            }
        }
        [HttpPut("ChangeProductStatus")]
        public async Task<IActionResult> changeProductStatus([FromBody]PutProductStatus dto)
        {
            try
            {
               var check =  await _productServices.changeProductStatus(dto);
                return check == true ? ApiResponseHelper.Success(Data: "Product is updated", StatusCode: 200) : ApiResponseHelper.Failure(Errors: "Faild to update product ", StatusCode: 200);
            }
            catch
            {
                return ApiResponseHelper.Failure(Errors: "Faild to update product ", StatusCode: 500);
            }
        }
    }
}
