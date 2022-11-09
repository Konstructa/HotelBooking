using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Guest> Get(int id)
        {
            return _hotelDbContext.Guests.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Domain.Entities.Guest>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Entities.Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
