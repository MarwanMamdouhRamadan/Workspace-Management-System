using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Common;
using Workspace.Application.DTOs;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SystemConstants.Roles.Admin)]
    public class RoomRateController : ControllerBase
    {
        IRoomRateServices _roomRate;

        public RoomRateController(IRoomRateServices roomRate)
        {
            _roomRate = roomRate;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var roomRates = await _roomRate.getAll();
            return ApiResponseHelper.Success(roomRates,StatusCode:200);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(long id)
        {
            var roomRate = await _roomRate.getById(id);
            return ApiResponseHelper.Success(roomRate,StatusCode:200);
        }
        [HttpPost()]
        public async Task<IActionResult> addRoomRate(RoomRateDto dto)
        {
            await _roomRate.addRoomRate(dto);
            return ApiResponseHelper.Success("Room rate is created", StatusCode: 200);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putRoomRate(long id,RoomRateDto dto)
        {
            await _roomRate.putRoomRate(id,dto);
            return ApiResponseHelper.Success("Room rate is updated", StatusCode: 200);
        }
    }
}
