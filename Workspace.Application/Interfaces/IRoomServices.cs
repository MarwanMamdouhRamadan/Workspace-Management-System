using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IRoomServices
    {
        Task<IEnumerable<RoomDto>> getAll();
        Task<RoomDto> getById(long id);
        Task<bool> addRoom(RoomDto dto);
        Task<bool> putRoom(long id, RoomDto dto);
        Task<bool> deleteRoom(long id);
        Task<IEnumerable<object>> getRoomByStatus(long statusId);
        Task<bool> changeRoomStatus(PutRoomStatus dto);
    }
}
