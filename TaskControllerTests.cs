using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Trello.Controllers;
using Trello.DataAccess;
using Trello.Interfaces;
using Trello.Models;

namespace TaskManagement
{
    [TestFixture]
    public class TaskControllerTests
    {
        private Mock<ApplicationDbContext> _mockDbContext;
        private Mock<TaskManagementController> _mockController;
        private Trello.Models.Task Task = new Trello.Models.Task();

        [SetUp]
        public void Setup()
        {
            var mockService = new Mock<ITaskService>();
            _mockDbContext = new Mock<ApplicationDbContext>();
            
            var mockDbSet = new Mock<DbSet<Trello.Models.Task>>();
            Task = new Trello.Models.Task()
            {
                Id = 2,
                Description = "Create Task",
                Status = "Progress",
                Title = "Contain Data",
                UserId = 437
            };
            mockDbSet.Setup(d => d.Add(Task));
            _mockDbContext.Setup(context => context.Tasks)
                .Returns(mockDbSet.Object);
            _mockController = new Mock<TaskManagementController>(_mockDbContext.Object, mockService.Object);
        }

        [Test]
        public void Test_CreateTask_ReturnsOkResult()
        {
            var result = _mockController.Object.CreateTask(new Task()
            {
                Id = 1,
                Description = "Create Task",
                Status = "Progress",
                Title = "Contain Data",
                UserId = 437
            });
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Test_GetTask_ReturnsOkResult()
        {
            var result = _mockController.Object.GetTask(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Test_UpdateTask_ReturnsOkResult()
        {
            var result = _mockController.Object.UpdateTask(Task);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}