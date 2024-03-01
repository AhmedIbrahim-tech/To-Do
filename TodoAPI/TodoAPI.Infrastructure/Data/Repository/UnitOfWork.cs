using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Domain.Interfaces;

namespace TodoAPI.Repository.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ITodoRepository Todo { get; }
        public HttpClient HttpClient { get; }
        public UnitOfWork(AppDbContext context, ITodoRepository todo, HttpClient httpClient)
        {
            _context = context;
            Todo = todo;
            HttpClient = httpClient;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
