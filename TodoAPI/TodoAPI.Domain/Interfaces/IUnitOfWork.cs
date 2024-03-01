using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoRepository Todo { get; }
        HttpClient HttpClient { get; }
        Task<int> CompleteAsync();
    }
}
