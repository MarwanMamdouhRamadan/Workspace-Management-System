using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Domain.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IRoomRateRepo :IGenricRepo<TbRoomRate>
    {
        public Task<IEnumerable<TbRoomRate>> getRoomRates();
        public Task<TbRoomRate> getRoomRateById(long id);
    }
}
