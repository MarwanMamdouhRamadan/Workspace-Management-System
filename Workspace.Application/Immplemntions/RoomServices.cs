using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Entities;
using static Workspace.Application.Common.SystemConstants;

namespace Workspace.Application.Immplemntions
{
    public class RoomServices : IRoomServices
    {
        IRoomRepo _repo;
        IStatusLookupService _lookupServices;

        public RoomServices(IRoomRepo genric, IStatusLookupService statusLookupService)
        {
            _repo = genric;
            _lookupServices = statusLookupService;
        }

        public async Task<bool> addRoom(RoomDto dto)
        {
            var status = _lookupServices.GetStatusId(StatusTypes.Room, Rooms.Active);
            if (status == 0)
                throw new KeyNotFoundException("System Error: 'Active' status configuration for rooms is missing.");

            TbRoom room = new TbRoom
            {
                Capacity = dto.Capacity,
                RoomName = dto.RoomName,
                StatusId = status,
            };
            await _repo.add(room);
            return true;
        }

        public async Task<bool> changeRoomStatus(PutRoomStatus dto)
        {
            var status = _lookupServices.getStatus(dto.statusId, StatusTypes.Room);
            if (status == null)
                throw new KeyNotFoundException($"The status ID {dto.statusId} is not valid for rooms.");

            var room = await _repo.getById(dto.roomId);
            if (room == null)
                throw new KeyNotFoundException($"Update failed: rooms with ID {dto.roomId} not found.");

            room.StatusId = status.id;
            _repo.update(room);
            return true;
        }

        public async Task<bool> deleteRoom(long id)
        {
            var room = await _repo.getRoomById(id);
            if (room == null)
                throw new KeyNotFoundException($"Delete failed: rooms with ID {id} not found.");

            var statusId = _lookupServices.GetStatusId(StatusTypes.Product, ProductStatus.Closed);
            if (statusId == 0)
                throw new KeyNotFoundException("System Error: 'Closed' status configuration is missing.");

            room.StatusId = statusId;
            _repo.update(room);
            return true;
        }

        public async Task<IEnumerable<RoomDto>> getAll()
        {
            var rooms = await _repo.getAllActivatedRooms();
            if (rooms == null || !rooms.Any())
                throw new KeyNotFoundException("No active rooms were found in the database.");

            return rooms.Select(x => new RoomDto
            {
                Capacity = x.Capacity,
                RoomName = x.RoomName,
            }).ToList();
        }

        public async Task<RoomDto> getById(long id)
        {
            var room = await _repo.getRoomById(id);
            if (room == null)
                throw new KeyNotFoundException($"Room with ID {id} was not found.");

            return new RoomDto
            {
                RoomName = room.RoomName,
                Capacity = room.Capacity,
            };
        }

        public async Task<IEnumerable<object>> getRoomByStatus(long statusId)
        {
            var rooms = await _repo.getAllRooms(statusId);
            if (rooms == null || !rooms.Any())
                throw new KeyNotFoundException($"No rooms found associated with status ID {statusId}.");

            var status = _lookupServices.getStatus(statusId, StatusTypes.Room);
            if (status == null)
                throw new KeyNotFoundException($"The status ID {statusId} is invalid or not registered for rooms.");

            return rooms.Select(x => new
            {
                x.RoomName,
                x.Capacity,
                Status = status.StatusName,
            }).ToList();
        }

        public async Task<bool> putRoom(long id, RoomDto dto)
        {
            var room = await _repo.getRoomById(id);
            if (room == null)
                throw new KeyNotFoundException($"Update failed: Product with ID {id} does not exist.");

            room.RoomName = dto.RoomName;
            room.Capacity = dto.Capacity;

            _repo.update(room);
            return true;
        }
    }
}
