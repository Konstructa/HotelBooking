
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Room> Get(int roomId);
        Task<int> Create(Room room);
        Task<Room> GetAggregate(int Id);
    }
}
