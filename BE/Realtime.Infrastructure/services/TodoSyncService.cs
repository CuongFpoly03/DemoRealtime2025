using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nest;
using Realtime.Infrastructure.Interfaces;
using Realtime.Infrastructure.Persistence.Repositories;

namespace Realtime.Infrastructure.services
{
    public class TodoSyncService
    {
        private readonly IRealtimeDbContext _dbcontext;
        private readonly TodoSearchRepository _elasticClient;
        public TodoSyncService(IRealtimeDbContext dbcontext, TodoSearchRepository elasticClient)
        {
            _dbcontext = dbcontext;
            _elasticClient = elasticClient;
        }
        // đồng bộ tất cả bản ghi todo vào elasticsearch 
        public async Task SyncTodosToElasticsearch()
        {
            var todos = await _dbcontext.Todos.ToListAsync();
            foreach (var item in todos)
            {
                await _elasticClient.IndexTodoAsync(item);
            }
        }
    }
}