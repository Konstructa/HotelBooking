﻿using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Entities.Room room)
        {
           _hotelDbContext.Rooms.Add(room);
           await _hotelDbContext.SaveChangesAsync();
           return room.Id;
        }

        public Task<Domain.Entities.Room> Get(int roomId)
        {
            return _hotelDbContext.Rooms.Where(x => x.Id == roomId).FirstAsync();
        }
    }
}
