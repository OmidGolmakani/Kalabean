using System;
using System.Threading.Tasks;

namespace Kalabean.Domain.Base
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
