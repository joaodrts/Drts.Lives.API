using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositorys
{
    public interface ILiveRepository
    {
        Task Add(Live live);
        Task Update(Live live);
        Task Remove(Live live);
        Task<IEnumerable<Live>> GetAll();
        Task<Live> GetByID(int id);
    }
}
