using EmployeeWebApp.Controllers;
using EmployeeWebApp.Data;
using EmployeeWebApp.Controllers;
using EmployeeWebApp.Models;
using EmployeeWebApp.Business;
using Microsoft.EntityFrameworkCore;
using EmployeeWebApp.Business;
using Moq;
using EmployeeWebApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAppTest
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeService> _employeeServiceMock;
        private EmployeesController _employeeController;
        [TestInitialize]
        public void Setup()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeesController(_employeeServiceMock.Object);
        }
        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto { Id = 1, Name = "John Doe" },
                new EmployeeDto { Id = 2, Name = "Jane Doe" }
            };
            _employeeServiceMock.Setup(service => service.GetAllEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeController.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<EmployeeDto>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }
        [TestMethod]
        public async Task Details_ReturnsViewResult_WithEmployee()
        {
            // Arrange
            var employee = new EmployeeDto { Id = 1, Name = "John Doe" };
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _employeeController.Details(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmployeeDto;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John Doe", model.Name);
        }
        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void Create_Get_ReturnsViewResult()
        {
            // Act
            var result = _employeeController.Create();

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public async Task Create_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var employee = new EmployeeDto { Id = 1, Name = "John Doe" };
            _employeeServiceMock.Setup(service => service.AddEmployeeAsync(employee)).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.Create(employee);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _employeeController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _employeeController.Create(new EmployeeDto());

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public async Task Edit_Get_ReturnsViewResult_WithEmployee()
        {
            // Arrange
            var employee = new EmployeeDto { Id = 1, Name = "John Doe" };
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _employeeController.Edit(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmployeeDto;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John Doe", model.Name);
        }

        [TestMethod]
        public async Task Edit_Get_ReturnsNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var employee = new EmployeeDto { Id = 1, Name = "John Doe" };
            _employeeServiceMock.Setup(service => service.UpdateEmployeeAsync(employee)).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.Edit(1, employee);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

    
        [TestMethod]
        public async Task Delete_Get_ReturnsViewResult_WithEmployee()
        {
            // Arrange
            var employee = new EmployeeDto { Id = 1, Name = "John Doe" };
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _employeeController.Delete(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmployeeDto;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John Doe", model.Name);
        }

        [TestMethod]
        public async Task Delete_Get_ReturnsNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync((EmployeeDto)null);

            // Act
            var result = await _employeeController.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteConfirmed_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            _employeeServiceMock.Setup(service => service.DeleteEmployeeAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

    }
}