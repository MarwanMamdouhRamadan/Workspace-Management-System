using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs.request;
using Workspace.Application.Utilities;
using Workspace.Infrastructure.Repositories.Interfaces;
using Workspace_Management_System.Entities;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTypeController : ControllerBase
    {
        IGenricRepo<TbStatusType> _genricRepo;
        ILookupService _lookupService;

        public StatusTypeController(IGenricRepo<TbStatusType> genricRepo, ILookupService lookupService)
        {
            _genricRepo = genricRepo;
            _lookupService = lookupService;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var result = _lookupService.GetAllCachedStatusTypes();
            if (result == null || !result.Any()) return  ApiResponseHelper.Failure(Errors: "Not found types yet", StatusCode: 404);
            return ApiResponseHelper.Success(result, StatusCode:200);
        }
        [HttpGet("search/{TypeName}")]
        public IActionResult getByName(string TypeName)
        {
            var result = _lookupService.GetAllCachedStatusTypes().FirstOrDefault(x => x.TypeName == TypeName);
            if (result == null ) return ApiResponseHelper.Failure(Errors: "Not found type", StatusCode: 404);
            return ApiResponseHelper.Success(result, StatusCode: 200);
        }
        [HttpPost]
        public async Task<IActionResult> addStatusType([FromBody] StatusTypeDto dto)
        {
            try
            {
                var statusType = new TbStatusType
                {
                    TypeName = dto.TypeName,
                };
                await _genricRepo.add(statusType);
                _lookupService.RefreshCache();
                return ApiResponseHelper.Success(Data: "The status type is created", StatusCode: 200);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Failure(Errors: "Faild to create status type", StatusCode: 500);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putStatusType(int id,[FromBody] StatusTypeDto dto)
        {
            try
            {
                var statusType = await _genricRepo.getById(id);
                if (statusType == null) return ApiResponseHelper.Failure(Errors: "Not found type", StatusCode: 404);
                statusType.TypeName = dto.TypeName;
                _genricRepo.update(statusType);
                _lookupService.RefreshCache();
                return ApiResponseHelper.Success(Data: "The status type is created", StatusCode: 200);
            }
            catch
            {
                return ApiResponseHelper.Failure(Errors: "Faild to create status type", StatusCode: 500);
            }
        }

    }
}
