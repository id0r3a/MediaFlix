using NUnit.Framework;
using ApplicationLayer.Services;
using ApplicationLayer.DTOs;
using MediaFlix.Tests.Fakes;
using DomainLayer.Models;
using System.Threading.Tasks;

namespace MediaFlix.Tests
{
    [TestFixture]
    public class MediaServiceTests
    {
        private MediaService _mediaService;
        private FakeMediaRepository _fakeRepo;

        [SetUp]
        public void Setup()
        {
            _fakeRepo = new FakeMediaRepository();
            _mediaService = new MediaService(_fakeRepo);
        }

        [Test]
        public async Task CreateAsync_ShouldReturnCreatedMedia()
        {
            var createDto = new CreateMediaDto
            {
                Title = "Test Title",
                Genre = "Action",
                Creator = "Director",
                Type = "Movie",
                Status = "Watched"
            };

            var result = await _mediaService.CreateAsync(createDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(createDto.Title, result.Title);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnMedia_WhenMediaExists()
        {
            // Arrange: skapa en media och lägg till i fake-repositoryn
            var media = new Media
            {
                Id = 1,
                UserId = 42,
                Title = "Test Title",
                Genre = "Action",
                Creator = "Director",
                Type = "Movie",
                Status = "Watched",
                CreatedAt = DateTime.UtcNow
            };
            await _fakeRepo.CreateAsync(media);

            // Act: hämta media via service
            var result = await _mediaService.GetByIdAsync(1, 42);

            // Assert: kolla att resultatet inte är null och har rätt titel
            Assert.IsNotNull(result);
            Assert.AreEqual(media.Title, result.Title);
        }

    }
}
