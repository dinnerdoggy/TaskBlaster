using Moq;
using TaskBlaster.Interfaces;
using TaskBlaster.Models;
using TaskBlaster.Services;
using Xunit;

namespace TaskBlaster.Tests.Services
{
    public class DutyServiceTests
    {
        private readonly Mock<IDutyRepository> _mockDutyRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly DutyService _service;
        private readonly string uid = "a";

        public DutyServiceTests()
        {
            _mockDutyRepo = new Mock<IDutyRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _service = new DutyService(_mockDutyRepo.Object, _mockCategoryRepo.Object);
        }

        [Fact]
        public async Task GetAllDutiesAsync_ReturnsDuties()
        {
            var duties = new List<Duty> { new Duty { Id = 1, Title = "Test", Uid = uid } };
            _mockDutyRepo.Setup(repo => repo.GetAllAsync(uid)).ReturnsAsync(duties);

            var result = await _service.GetAllDutiesAsync(uid);

            Xunit.Assert.Single(result);
            Xunit.Assert.Equal("Test", result.First().Title);
        }

        [Fact]
        public async Task GetDutyByIdAsync_ReturnsCorrectDuty()
        {
            var duty = new Duty { Id = 1, Title = "Test Duty", Uid = uid };
            _mockDutyRepo.Setup(repo => repo.GetByIdAsync(1, uid)).ReturnsAsync(duty);

            var result = await _service.GetDutyByIdAsync(1, uid);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Test Duty", result.Title);
        }

        [Fact]
        public async Task CreateDutyAsync_ReturnsCreatedDuty_WithResources()
        {
            var newDuty = new Duty
            {
                Title = "New Task",
                CategoryId = 1,
                Resources = new List<Resource>
        {
            new Resource { Id = 1 },
            new Resource { Id = 2 }
        }
            };
            var category = new Category { Id = 1, Uid = uid };

            _mockCategoryRepo.Setup(r => r.GetByIdAsync(1, uid)).ReturnsAsync(category);
            _mockDutyRepo.Setup(r => r.CreateAsync(It.Is<Duty>(d =>
                d.Uid == uid &&
                d.Resources != null &&
                d.Resources.Count == 2 &&
                d.Resources.Any(r => r.Id == 1) &&
                d.Resources.Any(r => r.Id == 2)
            ))).ReturnsAsync((Duty d) => d);

            var result = await _service.CreateDutyAsync(newDuty, uid);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("New Task", result.Title);
            Xunit.Assert.Equal(uid, result.Uid);
            Xunit.Assert.Equal(2, result.Resources.Count);
        }


        [Fact]
        public async Task UpdateDutyAsync_ReturnsUpdatedDuty_WithResources()
        {
            var updatePayload = new Duty
            {
                Title = "Updated Duty",
                Resources = new List<Resource>
        {
            new Resource { Id = 3 },
            new Resource { Id = 4 }
        }
            };

            _mockDutyRepo.Setup(r => r.UpdateAsync(1, updatePayload, uid))
                .ReturnsAsync(new Duty
                {
                    Id = 1,
                    Title = "Updated Duty",
                    Resources = updatePayload.Resources
                });

            var result = await _service.UpdateDutyAsync(1, updatePayload, uid);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Updated Duty", result.Title);
            Xunit.Assert.Equal(2, result.Resources.Count);
            Xunit.Assert.Contains(result.Resources, r => r.Id == 3);
            Xunit.Assert.Contains(result.Resources, r => r.Id == 4);
        }


        [Fact]
        public async Task DeleteDutyAsync_ReturnsTrue_WhenSuccessful()
        {
            _mockDutyRepo.Setup(repo => repo.DeleteAsync(1, uid)).ReturnsAsync(true);

            var result = await _service.DeleteDutyAsync(1, uid);

            Xunit.Assert.True(result);
        }

        [Fact]
        public async Task GetDutiesByCategoryIdAsync_ReturnsFilteredDuties()
        {
            var duties = new List<Duty> { new Duty { Id = 1, CategoryId = 5, Uid = uid } };
            _mockDutyRepo.Setup(r => r.GetDutiesByCategoryIdAsync(5, uid)).ReturnsAsync(duties);

            var result = await _service.GetDutiesByCategoryIdAsync(5, uid);

            Xunit.Assert.Single(result);
            Xunit.Assert.Equal(5, result.First().CategoryId);
        }

        [Fact]
        public async Task ToggleDutyCompletionAsync_ReturnsTrue_WhenSuccessful()
        {
            _mockDutyRepo.Setup(r => r.ToggleDutyCompletionAsync(1, uid)).ReturnsAsync(true);

            var result = await _service.ToggleDutyCompletionAsync(1, uid);

            Xunit.Assert.True(result);
        }
    }
}
