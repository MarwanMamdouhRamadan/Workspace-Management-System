using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.DTOs.response;

namespace Workspace.Infrastructure.Repositories.Interfaces
{
    public interface IStatusTypeLookupService
    {
        IEnumerable<StatusTypeDtoResponse> GetAllCachedStatusTypes();
        void RefreshCache();
    }
}
