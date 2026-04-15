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
    public class BookingRepo : GenricRepo<TbBooking>, IBookingRepo
    {
        public BookingRepo(WorkSpaceSysContext db) : base(db)
        {
        }

        public async Task<bool> isReserved(DateTime start, DateTime end ,string mode,long roomId ,int countOfPeople)
        {
            if (mode.Equals(SystemConstants.RoomType.Private,StringComparison.OrdinalIgnoreCase) || mode.Equals(SystemConstants.RoomType.Private_Group, StringComparison.OrdinalIgnoreCase))
            {
                return await _dbSet.Include(x => x.Room).AnyAsync(x => (x.Room.Capacity < countOfPeople)&&(x.RoomId == roomId) && (x.StartTime < end && x.EndTime > start));
            }
            else
            {
                var total =  await _dbSet.Include(x => x.Room)
                    .ThenInclude(x => x.RoomRates)
                    .Where(x => x.Room.RoomRates
                    .Any(x => (x.RoomId == roomId)&& (x.Mode == SystemConstants.RoomType.Shared)))
                    .Where(x =>  x.StartTime < end && x.EndTime > start)
                    .SumAsync(x => x.CountOfPeople);
                return await _dbSet.Include(x => x.Room).Where(x => x.RoomId == roomId).AnyAsync(x => (x.Room.Capacity < total + countOfPeople ) && (x.StartTime < end && x.EndTime > start));
            }
        }
    }
}
