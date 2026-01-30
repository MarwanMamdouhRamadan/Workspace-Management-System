using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;
using Workspace_Management_System.Data;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class RoomRateRepo : GenricRepo<TbRoomRate>, IRoomRateRepo
    {
        public RoomRateRepo(WorkSpaceSysContext db) : base(db)
        {
        }

        public async Task<TbRoomRate> getRoomRateById(long id)
        {
            return await _dbSet.Include(r => r.Room).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TbRoomRate>> getRoomRates()
        {
            return await _dbSet.Include(r => r.Room).ToListAsync();
        }
    }
}
