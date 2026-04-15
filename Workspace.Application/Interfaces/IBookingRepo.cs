using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IBookingRepo : IGenricRepo<TbBooking>
    {
        public Task<bool> isReserved(DateTime start , DateTime end , string mode, long roomId,int countOfPeople);
    }
}
