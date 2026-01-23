using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs.request;
using Workspace.Application.Utilities;
using Workspace.Infrastructure.Repositories.Interfaces;
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
            var result = _lookupService.GetAllCachedStatusTypes();
            if (result == null || !result.Any()) return ApiResponseHelper.Failure(Errors: "Not found status yet", StatusCode: 404);
            return ApiResponseHelper.Success(result, StatusCode: 200);
        }
        [HttpGet("search/{TypeName}")]
        public IActionResult getByName(string TypeName)
        {
            var result = _lookupService.GetAllCachedStatusTypes().FirstOrDefault(x => x.StatusName.ToLower().Trim() == TypeName.ToLower().Trim());
            if (result == null) return ApiResponseHelper.Failure(Errors: "Not found status", StatusCode: 404);
            return ApiResponseHelper.Success(result, StatusCode: 200);
        }
        [HttpPost]
        public async Task<IActionResult> addStatusType([FromBody] StatusDto dto)
        {
            try
            {
                var statusType = new TbStatus
                {
                    StatusName = dto.StatusName,
                    StatusTypeId = dto.StatusTypeId,
                };
                await _genricRepo.add(statusType);
                _lookupService.RefreshCache();
                return ApiResponseHelper.Success(Data: "The status  is created", StatusCode: 200);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Failure(Errors: "Faild to create status ", StatusCode: 500);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putStatusType(int id, [FromBody] StatusDto dto)
        {
            try
            {
                var statusType = await _genricRepo.getById(id);
                if (statusType == null) return ApiResponseHelper.Failure(Errors: "Not found status", StatusCode: 404);
                statusType.StatusName = dto.StatusName;
                _genricRepo.update(statusType);
                _lookupService.RefreshCache();
                return ApiResponseHelper.Success(Data: "The status  is updated", StatusCode: 200);
            }
            catch
            {
                return ApiResponseHelper.Failure(Errors: "Faild to update status ", StatusCode: 500);
            }
        }

    }
}
