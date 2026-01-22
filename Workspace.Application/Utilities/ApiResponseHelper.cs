using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Workspace.Application.Utilities
{
    public static class ApiResponseHelper
    {
        public static IActionResult Success(object Data , int StatusCode = 200)
        {
           return new ObjectResult(
               new ApiResponse
               {
                   Data = Data,
                   Errors = null,
                   StatusCode = StatusCode
               }
               )
           {
               StatusCode = StatusCode
           };
                
        }
        public static IActionResult Failure(object Errors ,int StatusCode = 400)
        {
            return new ObjectResult(
                new ApiResponse
                {
                    Data = null,
                    Errors = Errors,
                    StatusCode = StatusCode
                }
                )
            {
                StatusCode = StatusCode
            };
        }
    }
}
