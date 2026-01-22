using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs;
using Workspace_Managment_System.identity;

namespace Workspace.Application.Interfaces
{
    public interface IAuthServiecs
    {
         Task<AuthResponseDto> SignUp(SignUpDto dto);
         Task<AuthResponseDto> SignIn(SignInDto dto);

    }
}
