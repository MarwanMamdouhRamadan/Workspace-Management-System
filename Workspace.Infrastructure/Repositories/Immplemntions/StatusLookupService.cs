using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Workspace.Application.DTOs.response;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Data;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class StatusLookupService :IStatusLookupService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly object _lock = new();

        private Dictionary<long, StatusDtoResponse> _statusCache = new();
        private Dictionary<long, StatusTypeDtoResponse> _typeCache = new();

        public StatusLookupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            RefreshCache();
        }

        public void RefreshCache()
        {
            lock (_lock)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<WorkSpaceSysContext>();

                    _typeCache = db.TbStatusTypes
                        .Select(t => new StatusTypeDtoResponse
                        {
                            id = t.Id,
                            TypeName = t.TypeName
                        }).ToDictionary(t => t.id);

                    _statusCache = db.TbStatuses
                        .Select(s => new StatusDtoResponse
                        {
                            id = s.Id,
                            StatusName = s.StatusName,
                            StatusTypeId = s.StatusTypeId
                        }).ToDictionary(s => s.id);
                }
            }
        }

        public long GetStatusId(string typeName, string statusName)
        {
            var type = _typeCache.Values
                .FirstOrDefault(t => t.TypeName.Equals(typeName, StringComparison.OrdinalIgnoreCase));

            if (type == null) return 0;

            var status = _statusCache.Values
                .FirstOrDefault(s => s.StatusName.Equals(statusName, StringComparison.OrdinalIgnoreCase)
                                     && s.StatusTypeId == type.id);

            return status?.id ?? 0;
        }

        public IEnumerable<StatusDtoResponse> GetStatuses() => _statusCache.Values;

        public IEnumerable<StatusTypeDtoResponse> GetStatusTypes() => _typeCache.Values;

        public StatusDtoResponse getStatus(long statusId, long statusTypeId)
        {
            var status = _statusCache.Values.FirstOrDefault(x => x.id == statusId && x.StatusTypeId == statusTypeId);
            return status;
        }

        public StatusDtoResponse getStatus(long statusId, string typeName)
        {
            var type = _typeCache.Values
                .FirstOrDefault(x => x.TypeName.Equals(typeName,StringComparison.OrdinalIgnoreCase));
            var status = _statusCache.Values
                .FirstOrDefault(x => x.id == statusId && x.StatusTypeId == type.id);
            return status;
        }
    }
}
