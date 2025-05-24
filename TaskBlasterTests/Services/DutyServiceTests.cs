using Moq;
using TaskBlaster.Interfaces;
using TaskBlaster.Models;
using TaskBlaster.Services;
using Xunit;

namespace TaskBlaster.Tests.Services
{
    public class DutyServiceTests
    {
        private readonly Mock<IDutyRepository> _mockRepo;
        private readonly DutyService _service;

        public DutyServiceTests()
        {
            _mockRepo = new Mock<IDutyRepository>();
            _service = new DutyService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllDutiesAsync_ReturnsDuties()
        {
            // Arrange
            var duties = new List<Duty> { new Duty { Id = 1, Title = "Test" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(duties);

            // Act
            var result = await _service.GetAllDutiesAsync();

            // Assert
            Xunit.Assert.Single(result);
            Xunit.Assert.Equal("Test", result.First().Title);
        }

        [Fact]
        public async Task GetDutyByIdAsync_ReturnsCorrectDuty()
        {
            // Arrange
            var duty = new Duty { Id = 1, Title = "Test Duty" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(duty);

            // Act
            var result = await _service.GetDutyByIdAsync(1);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Test Duty", result.Title);
        }

        [Fact]
        public async Task CreateDutyAsync_ReturnsCreatedDuty()
        {
            // Arrange
            var newDuty = new Duty { Title = "New Task" };
            _mockRepo.Setup(repo => repo.CreateAsync(newDuty)).ReturnsAsync(new Duty { Id = 99, Title = "New Task" });

            // Act
            var result = await _service.CreateDutyAsync(newDuty);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("New Task", result.Title);
            Xunit.Assert.Equal(99, result.Id);
        }

        [Fact]
        public async Task UpdateDutyAsync_ReturnsUpdatedDuty()
        {
            // Arrange
            var updated = new Duty { Title = "Updated" };
            _mockRepo.Setup(repo => repo.UpdateAsync(1, updated)).ReturnsAsync(new Duty { Id = 1, Title = "Updated" });

            // Act
            var result = await _service.UpdateDutyAsync(1, updated);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Updated", result.Title);
        }

        [Fact]
        public async Task DeleteDutyAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteDutyAsync(1);

            // Assert
            Xunit.Assert.True(result);
        }

        [Fact]
        public async Task GetDutiesByCategoryIdAsync_ReturnsFilteredDuties()
        {
            // Arrange
            var duties = new List<Duty> { new Duty { Id = 1, CategoryId = 5 } };
            _mockRepo.Setup(r => r.GetDutiesByCategoryIdAsync(5)).ReturnsAsync(duties);

            // Act
            var result = await _service.GetDutiesByCategoryIdAsync(5);

            // Assert
            Xunit.Assert.Single(result);
            Xunit.Assert.Equal(5, result.First().CategoryId);
        }

        [Fact]
        public async Task ToggleDutyCompletionAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            _mockRepo.Setup(r => r.ToggleDutyCompletionAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.ToggleDutyCompletionAsync(1);

            // Assert
            Xunit.Assert.True(result);
        }
    }
}
