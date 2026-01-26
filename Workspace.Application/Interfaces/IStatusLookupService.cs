using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.response;

namespace Workspace.Application.Interfaces
{
    public interface IStatusLookupService
    {
        IEnumerable<StatusDtoResponse> GetStatuses();
        IEnumerable<StatusTypeDtoResponse> GetStatusTypes();
        long GetStatusId(string typeName, string statusName);
        StatusDtoResponse getStatus(long statusId, long statusTypeId);
        StatusDtoResponse getStatus(long statusId, string typeName);
        void RefreshCache();
    }
}
