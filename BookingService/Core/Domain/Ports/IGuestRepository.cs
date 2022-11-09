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
        Task<int> Create(Guest guest);
        Task<Guest> Get(int id);
        Task Delete(int id);
        Task Update(Guest guest);
        Task<IEnumerable<Guest>> GetAll();

    }
}
