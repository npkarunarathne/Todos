using Todos.Models;
using Todos.Repositories.Interfaces;

namespace Todos.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodosDbContext _context;
        private readonly IConfiguration Configuration;

        public TodoRepository(TodosDbContext context, IConfiguration configuration)
        {

            _context = context;
            Configuration = configuration;
        }

        public async Task<List<Todo>> All()
        {
            try
            { 

                var result = _context.Todo.ToList();

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Todo> Get(int id)
        {
            try
            {
                var result = _context.Todo.Where(x => x.Id == id).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Todo does not exist");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<Todo> Create(Todo dto)
        {
            try
            {
                if (dto != null)
                {
                    await _context.Todo.AddAsync(dto);
                    await _context.SaveChangesAsync();

                    return dto;

                }
                else
                {
                    throw new Exception("insert data was null");
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public async Task<Todo> Update(int id, Todo dto)
        {
            try
            {
                var result = _context.Todo.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    result.Title = dto.Title;
                    result.Description = dto.Description;
                    result.IsCompleted = dto.IsCompleted;

                    await _context.SaveChangesAsync();

                    return result;
                }
                else
                {
                    throw new Exception("Todo is not exist");

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<Todo> Delete(int id)
        {
            try
            {
                var result = _context.Todo.Where(x => x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    _context.Todo.Remove(result);
                    await _context.SaveChangesAsync();

                    return result;
                }
                else
                {

                    throw new Exception("Todo is not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }

}
