using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.DTOs.response;
using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;

namespace Workspace.Application.Immplemntions
{
    public class RoomRateServices : IRoomRateServices
    {
        IUnitOfWork _repo;

        public RoomRateServices(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task<bool> addRoomRate(RoomRateDto dto)
        {
            TbRoomRate roomRate = new TbRoomRate()
            {
                HourlyRate = dto.HourlyRate,
                Mode = dto.Mode,
                RoomId = dto.RoomId,
            };
            await _repo.roomRateRepo.add(roomRate);
            await _repo.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<RoomRateResponseDto>> getAll()
        {
            var roomRates = await _repo.roomRateRepo.getRoomRates();
            if (roomRates == null || !roomRates.Any()) return Enumerable.Empty<RoomRateResponseDto>();
            return roomRates.Select(x => new RoomRateResponseDto
            {
                HourlyRate= x.HourlyRate,
                Mode= x.Mode,
                RoomName = x.Room.RoomName ?? "N/A"
            }).ToList();
        }

        public async Task<RoomRateResponseDto> getById(long id)
        {
            var roomRate = await _repo.roomRateRepo.getRoomRateById(id);
            if (roomRate == null) throw new KeyNotFoundException($"Room rate with ID {id} was not found.");
            return new RoomRateResponseDto
            {
                HourlyRate = roomRate.HourlyRate,
                Mode = roomRate.Mode,
                RoomName = roomRate.Room.RoomName ?? "N/A",
            };
        }

        public async Task<bool> putRoomRate(long id, RoomRateDto dto)
        {
            var roomRate = await _repo.roomRateRepo.getById(id);
            if (roomRate == null) throw new KeyNotFoundException($"Room rate with ID {id} was not found.");
            roomRate.HourlyRate = dto.HourlyRate;
            roomRate.Mode = dto.Mode;
            roomRate.RoomId = dto.RoomId;
            _repo.roomRateRepo.update(roomRate);
            await _repo.CompleteAsync();
            return true;
        }
    }
}
