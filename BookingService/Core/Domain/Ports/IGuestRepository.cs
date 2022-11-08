using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Guest> Get(int id);
        Task<IEnumerable<Guest>> GetAll();
        Task Delete(int id);
        Task Update(Guest guest);
        Task<int> Salve(Guest guest);
    }
}
