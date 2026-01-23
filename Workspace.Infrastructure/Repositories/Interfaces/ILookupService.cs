using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;

namespace Workspace.Infrastructure.Repositories.Interfaces
{
    public interface ILookupService
    {
        IEnumerable<StatusTypeDto> GetAllCachedStatusTypes();
        void RefreshCache();
    }
}
