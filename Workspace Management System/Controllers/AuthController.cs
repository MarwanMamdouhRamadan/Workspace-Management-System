using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;
using Workspace.Infrastructure;

namespace Workspace_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiecs _authServiecs;

        public AuthController(IAuthServiecs authServiecs)
        {
            _authServiecs = authServiecs;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Sigup([FromBody] SignUpDto dto)
        {
           var result = await _authServiecs.SignUp(dto);
           if(result.IsSuccess == false)
           {
                return ApiResponseHelper.Failure(Errors: result.Errors == null ? result.Message : result.Errors, StatusCode: 401);
           }
            return ApiResponseHelper.Success(Data: result.Message, StatusCode: 201);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var result = await _authServiecs.SignIn(dto);
            if (result.IsSuccess == false)
            {
                return ApiResponseHelper.Failure(Errors: result.Errors == null ? result.Message : result.Errors, StatusCode: 401);
            }
            return ApiResponseHelper.Success(Data: new {token = result.Data,message = result.Message}, StatusCode: 200);
        }
    }
}
