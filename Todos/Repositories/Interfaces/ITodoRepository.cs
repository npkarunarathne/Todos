using Todos.Models;

namespace Todos.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<List<Todo>> All();
        public Task<Todo> Get(int id);
        public Task<Todo> Create(Todo dto);
        public Task<Todo> Update(int id, Todo dto);
        public Task<Todo> Delete(int id);
    }
}
