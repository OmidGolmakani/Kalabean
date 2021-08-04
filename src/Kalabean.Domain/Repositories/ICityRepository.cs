using System;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface ICityRepository: IRepository<City>
    {
        Task<City> GetById (int id, bool includeDeleted=false);
    }
}
