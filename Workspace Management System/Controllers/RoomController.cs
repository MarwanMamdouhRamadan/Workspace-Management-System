using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        [HttpGet("GetRoomByStatus/{statusId}")]
        public async Task<IActionResult> getProductsByStatus(long statusId)
        {
            var products = await _roomServices.getRoomByStatus(statusId);
            return ApiResponseHelper.Success(products, StatusCode: 200);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var products = await _roomServices.getAll();
            return ApiResponseHelper.Success(products, StatusCode: 200);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(long id)
        {
            var product = await _roomServices.getById(id);
            return ApiResponseHelper.Success(Data: product, StatusCode: 200);
        }
        [HttpPost]
        public async Task<IActionResult> addProduct([FromBody] RoomDto dto)
        {
            await _roomServices.addRoom(dto);
            return ApiResponseHelper.Success(Data: "Room is craeted", StatusCode: 200);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putProduct(long id, [FromBody] RoomDto dto)
        {
            await _roomServices.putRoom(id, dto);
            return ApiResponseHelper.Success(Data: "Room is updated", StatusCode: 200);
        }
        [HttpPut("DeleteProduct/{id}")]
        public async Task<IActionResult> deleteProduct(long id)
        {
            await _roomServices.deleteRoom(id);
            return ApiResponseHelper.Success(Data: "Room is deleted", StatusCode: 200);
        }
        [HttpPut("ChangeProductStatus")]
        public async Task<IActionResult> changeProductStatus([FromBody] PutRoomStatus dto)
        {
            await _roomServices.changeRoomStatus(dto);
            return ApiResponseHelper.Success(Data: "Room is updated", StatusCode: 200);
        }
    }
}
