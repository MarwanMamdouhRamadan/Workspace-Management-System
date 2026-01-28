using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Common;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Data;
using Workspace_Management_System.Entities;

namespace Workspace.Infrastructure.Repositories.Immplemntions
{
    public class RoomRepo : GenricRepo<TbRoom>, IRoomRepo
    {
        public RoomRepo(WorkSpaceSysContext db) : base(db)
        {
        }

        public async Task<IEnumerable<TbRoom>> getAllActivatedRooms()
        {
            return await _dbSet.Include(x => x.Status).Where(x => x.Status.StatusName == SystemConstants.Rooms.Active).ToListAsync();
        }

        public async Task<IEnumerable<TbRoom>> getAllRooms(long statusId)
        {
            return await _dbSet.Where(x => x.StatusId == statusId).ToListAsync();
        }

        public async Task<TbRoom> getRoomById(long id)
        {
            return await _dbSet.Include(x => x.Status).Where(x => x.Status.StatusName == SystemConstants.Rooms.Active).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
