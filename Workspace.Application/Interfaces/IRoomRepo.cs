using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IRoomRepo : IGenricRepo<TbRoom>
    {
        Task<IEnumerable<TbRoom>> getAllActivatedRooms();
        Task<IEnumerable<TbRoom>> getAllRooms(long statusId);
        Task<TbRoom> getRoomById(long id);
    }
}
