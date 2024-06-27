using Microsoft.AspNetCore.Mvc;
using Todos.Models;
using Todos.Repositories.Interfaces;

namespace Todos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository TodoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            TodoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            try
            {
                var result = await TodoRepository.All();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var result = await TodoRepository.Get(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Todo dto)
        {
            try
            {
                var result = await TodoRepository.Create(dto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Todo dto)
        {
            try
            {
                var result = await TodoRepository.Update(id, dto);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await TodoRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}
