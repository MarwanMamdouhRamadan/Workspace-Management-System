using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.DTOs.response;

namespace Workspace.Application.Interfaces
{
    public interface IRoomRateServices
    {
        Task<IEnumerable<RoomRateResponseDto>> getAll();
        Task<RoomRateResponseDto> getById(long id);
        Task<bool> addRoomRate(RoomRateDto product);
        Task<bool> putRoomRate(long id, RoomRateDto dto);
    }
}
