using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Workspace.Application.DTOs;
using Workspace.Application.Interfaces;
using Workspace.Application.Utilities;
using Workspace.Infrastructure.Repositories.Interfaces;
using Workspace_Managment_System.identity;
using Microsoft.AspNetCore.Identity;
namespace Workspace.Application.Services
{
    public class AuthServiecs : IAuthServiecs
    {
        private readonly  Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthServiecs> _logger;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public AuthServiecs(UserManager<ApplicationUser> userManager, ILogger<AuthServiecs> logger, IJwtService jwtService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckUserExists(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                _logger.LogWarning("SignUp failed: Email {Email} already exists at Date : #{Date}",Email,DateTime.UtcNow);
                return false;
            }
            return true;
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser User, string Password)
        {
            var result = await _userManager.CreateAsync(User,Password);
            if(!result.Succeeded)
            {
                _logger.LogError("SignUp failed for email #{Email}. Errors: #{Errors} at Date: #{Date}", User.Email, result.Errors, DateTime.UtcNow);
                
            }
            return result;
        }

        public async Task<AuthResponseDto> SignIn(SignInDto dto)
        {
            _logger.LogInformation("SignIn attempt for email: #{Email} at Date: #{Date}", dto.Email, DateTime.UtcNow);
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null)
            {
                _logger.LogWarning("User is not exists with this email : #{Email}", dto.Email);
                return new AuthResponseDto()
                {
                    IsSuccess = false,
                    Message = "User is not exists with this email"
                };
            }
            var result = await _signInManager.PasswordSignInAsync(user,dto.Password,true,true);
            if(!result.Succeeded)
            {
                return new AuthResponseDto()
                {
                    IsSuccess = false,
                    Message = "Sign In failed"
                };
            }
            var token =await _jwtService.GenerateToken(user);
            return new AuthResponseDto()
            {
                Data = token,
                IsSuccess = true,
                Message = "SignIn Succesfully"
            };
            
        }

        public async Task<AuthResponseDto> SignUp(SignUpDto dto)
        {
            _logger.LogInformation("SignUp attempt for email: #{Email} at Date: #{Date}", dto.Email, DateTime.UtcNow);
            var checkUserExists = await CheckUserExists(dto.Email);
            if (checkUserExists == false) return new AuthResponseDto()
            {
                Data = null,
                IsSuccess = false,
                Message = "User with this email already exists",
                Errors = null,

            };

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName,
            };
            var result = await CreateUser(user, dto.Password);
            if (!result.Succeeded) return new AuthResponseDto()
            {
                Data = null,
                IsSuccess = false,
                Message = "",
                Errors = result.Errors.Select(e => e.Description),
            };
            await _userManager.AddToRoleAsync(user, "Customer");
            return new AuthResponseDto
            {
                Data = null,
                Errors = null,
                Message = "Account created successfully.",
                IsSuccess = true
            };
        }
    }
}
