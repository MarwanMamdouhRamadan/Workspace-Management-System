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
    public class StatusController : ControllerBase
    {
        IGenricRepo<TbStatus> _genricRepo;
        IStatusLookupService _lookupService;

        public StatusController(IGenricRepo<TbStatus> genricRepo, IStatusLookupService lookupService)
        {
            _genricRepo = genricRepo;
            _lookupService = lookupService;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var result = _lookupService.GetStatuses();
            if (result == null || !result.Any())
                throw new KeyNotFoundException("No status records found.");
            return ApiResponseHelper.Success(result, StatusCode: 200);
        }
        [HttpGet("search/{TypeName}")]
        public IActionResult getByName(string TypeName)
        {
            var result = _lookupService.GetStatuses().FirstOrDefault(x => x.StatusName.ToLower().Trim() == TypeName.ToLower().Trim());
            if (result == null)
                throw new KeyNotFoundException($"Status with name '{TypeName}' was not found.");
            return ApiResponseHelper.Success(result, StatusCode: 200);
        }
        [HttpPost]
        public async Task<IActionResult> addStatusType([FromBody] StatusDto dto)
        {
            var statusType = new TbStatus
            {
                StatusName = dto.StatusName,
                StatusTypeId = dto.StatusTypeId,
            };

            await _genricRepo.add(statusType);
            _lookupService.RefreshCache();

            return ApiResponseHelper.Success("The status is created");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putStatusType(int id, [FromBody] StatusDto dto)
        {
            var statusType = await _genricRepo.getById(id);

            if (statusType == null)
                throw new KeyNotFoundException($"Status with ID {id} not found.");

            statusType.StatusName = dto.StatusName;
            _genricRepo.update(statusType);
            _lookupService.RefreshCache();

            return ApiResponseHelper.Success("The status is updated");
        }

    }
}
