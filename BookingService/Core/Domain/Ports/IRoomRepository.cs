
using Domain.Entities;

namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Room> Get(int roomId);
        Task<int> Create(Room room);
    }
}
