// src/TasksAPI.Tests/Controllers/TasksControllerTests.cs
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TaskService.Interface;
using TasksAPI.Controllers;
using TaskService;
using Assert = Xunit.Assert;

namespace Controller_Test
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskinterface> _mockTaskService;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockTaskService = new Mock<ITaskinterface>();
            _controller = new TasksController(_mockTaskService.Object);
        }

        // Test for GET /api/tasks
        [Fact]
        public async Task Get_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<Tasks>
            {
                new Tasks { Id = 1, Name = "Test Task 1", Status = "Description 1" },
                new Tasks {Id = 2, Name = "Test Task 2", Status = "Description 2"}
            };
            _mockTaskService.Setup(service => service.GetTasks()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Tasks>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Tasks>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        // Test for POST /api/tasks
        [Fact]
        public async Task Post_CreatesTask_ReturnsCreatedResult()
        {
            // Arrange
            var newTask = new Tasks { Id = 3, Name = "New Task", Status = "New Task Description" };
            //_mockTaskService.Setup(service => service.CreateTasks(It.IsAny<Tasks>())).ReturnsAsync(newTask);

            // Act
            var result = await _controller.Post(newTask);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Tasks>(createdResult.Value);
            Assert.Equal("New Task", returnValue.Name);
        }

        // Test for PUT /api/tasks/{taskid}
        [Fact]
        public async Task Put_ValidTask_ReturnsOkResult()
        {
            // Arrange
            var taskToUpdate = new Tasks { Id = 1, Name = "Updated Task", Status = "Updated Description" };
            //_mockTaskService.Setup(service => service.EditTasks(It.IsAny<Tasks>())).ReturnsAsync(taskToUpdate);

            // Act
            var result = await _controller.Put(taskToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Tasks>(okResult.Value);
            Assert.Equal("Updated Task", returnValue.Name);
        }

        // Test for PUT /api/tasks/{taskid} when task ID does not match
        [Fact]
        public async Task Put_TaskIdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var taskToUpdate = new Tasks { Id = 1, Name = "Updated Task", Status = "Updated Description" };

            // Act
            var result = await _controller.Put(taskToUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test for DELETE /api/tasks/{taskid}
        [Fact]
        public async Task Delete_ValidTaskId_ReturnsNoContent()
        {
            // Arrange
            int taskIdToDelete = 1;
            //_mockTaskService.Setup(service => service.DeleteTasks(taskIdToDelete)).ReturnsAsync(true);

            // Act
            await _controller.Delete(taskIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(null);
        }

        // Test for DELETE /api/tasks/{taskid} when task is not found
        [Fact]
        public async Task Delete_TaskNotFound_ReturnsNotFound()
        {
            // Arrange
            var taskIdToDelete = 1;
            // _mockTaskService.Setup(service => service.DeleteTasks(taskIdToDelete)).ReturnsAsync(false);

            // Act
            await _controller.Delete(taskIdToDelete);

            // Assert
            Assert.IsType<NotFoundObjectResult>(null);
        }
    }
}
