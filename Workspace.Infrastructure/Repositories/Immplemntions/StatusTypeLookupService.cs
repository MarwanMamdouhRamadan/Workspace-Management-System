using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Infrastructure.Repositories.Interfaces;
using Workspace_Management_System.Data;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class StatusTypeLookupService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly object _look = new();
        public Dictionary<long, string>? cache { get; set; }
        public StatusTypeLookupService(IServiceScopeFactory _scopeFactory)
        {
            this._scopeFactory  = _scopeFactory;
            RefreshCache();
        }
        public IEnumerable<StatusTypeDto> GetAllCachedStatusTypes()
        {
            return cache.Select(x => new StatusTypeDto { TypeName = x.Value });
        }

        public void RefreshCache()
        {
            lock (_look)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<WorkSpaceSysContext>();
                    cache = db.TbStatusTypes.ToDictionary(x => x.Id, x => x.TypeName);
                }
            }
        }
    }
}
