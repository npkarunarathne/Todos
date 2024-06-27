using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Todos.Models;
using Todos.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework.Legacy;

namespace UnitTests
{
    [TestFixture]
    public class TodoRepositoryTests
    {
        private TodosDbContext _context;
        private TodoRepository _repository;
        private List<Todo> _todos;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TodosDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new TodosDbContext(options);

            _todos = new List<Todo>
            {
                new Todo { Id = 1, Title = "Todo 1", Description = "Description 1", IsCompleted = false },
                new Todo { Id = 2, Title = "Todo 2", Description = "Description 2", IsCompleted = true }
            };

            _context.Todo.AddRange(_todos);
            _context.SaveChanges();

            _repository = new TodoRepository(_context, Mock.Of<IConfiguration>());
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AllShouldReturnAllTodos()
        {
            var result = await _repository.All();

            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.AreEqual("Todo 1", result[0].Title);
            ClassicAssert.AreEqual("Todo 2", result[1].Title);
        }

        [Test]
        public async Task Get_ShouldReturnTodo_WhenTodoExists()
        {
            var result = await _repository.Get(1);

            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(1, result.Id);
        }

        [Test]
        public void Get_ShouldThrowException_WhenTodoDoesNotExist()
        {
            var ex = ClassicAssert.ThrowsAsync<Exception>(async () => await _repository.Get(3));
            ClassicAssert.AreEqual("Todo does not exist", ex.Message);
        }

        [Test]
        public async Task Create_ShouldAddNewTodo()
        {
            var newTodo = new Todo { Id = 3, Title = "Todo 3", Description = "Description 3", IsCompleted = false };

            var result = await _repository.Create(newTodo);

            var createdTodo = _context.Todo.Find(3);

            ClassicAssert.NotNull(createdTodo);
            ClassicAssert.AreEqual(newTodo.Title, createdTodo.Title);
            ClassicAssert.AreEqual(newTodo.Description, createdTodo.Description);
            ClassicAssert.AreEqual(newTodo.IsCompleted, createdTodo.IsCompleted);
        }

        [Test]
        public async Task Update_ShouldModifyExistingTodo()
        {
            var updatedTodo = new Todo { Id = 1, Title = "Updated Todo 1", Description = "Updated Description 1", IsCompleted = true };

            var result = await _repository.Update(1, updatedTodo);

            var modifiedTodo = _context.Todo.Find(1);

            ClassicAssert.AreEqual("Updated Todo 1", modifiedTodo.Title);
            ClassicAssert.AreEqual("Updated Description 1", modifiedTodo.Description);
            ClassicAssert.IsTrue(modifiedTodo.IsCompleted);
        }

        [Test]
        public void Update_ShouldThrowException_WhenTodoDoesNotExist()
        {
            var updatedTodo = new Todo { Id = 3, Title = "Non-Existent Todo", Description = "Non-Existent Description", IsCompleted = true };

            var ex = ClassicAssert.ThrowsAsync<Exception>(async () => await _repository.Update(3, updatedTodo));
            ClassicAssert.AreEqual("Todo is not exist", ex.Message);
        }


        [Test]
        public async Task Delete_ShouldRemoveExistingTodo()
        {
            var result = await _repository.Delete(1);

            var deletedTodo = _context.Todo.Find(1);

            ClassicAssert.IsNull(deletedTodo);
        }

        [Test]
        public void Delete_ShouldThrowException_WhenTodoDoesNotExist()
        {
            var ex = ClassicAssert.ThrowsAsync<Exception>(async () => await _repository.Delete(3));
            ClassicAssert.AreEqual("Todo is not exist", ex.Message);
        }

    }
}
