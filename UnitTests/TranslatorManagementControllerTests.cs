using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Controllers.Tests
{
    public class TranslatorManagementControllerTests
    {
        private readonly Mock<IDataRepository> mockRepository;
        private readonly Mock<ILogger<TranslatorManagementController>> mockLogger;
        private readonly TranslatorManagementController controller;

        public TranslatorManagementControllerTests()
        {
            mockRepository = new Mock<IDataRepository>();
            mockLogger = new Mock<ILogger<TranslatorManagementController>>();
            controller = new TranslatorManagementController(mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void GetTranslatorsByName_WithValidName_ReturnsOkWithMatchingTranslators()
        {
            // Arrange
            var name = "John";
            var translators = new[]
            {
                new Translator { Name = name },
                new Translator { Name = "Jane" },
                new Translator { Name = name }
            };
            mockRepository.Setup(repo => repo.Translators).Returns(translators.AsQueryable());

            // Act
            var result = controller.GetTranslatorsByName(name);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTranslators = Assert.IsAssignableFrom<Translator[]>(okResult.Value);
            returnedTranslators.Should().HaveCount(2);
            returnedTranslators.Should().OnlyContain(t => t.Name == name);
        }

        [Fact]
        public void GetTranslatorsByName_WithNullName_ReturnsOkWithAllTranslators()
        {
            // Arrange
            var translators = new[]
            {
                new Translator { Name = "John" },
                new Translator { Name = "Jane" }
            };
            mockRepository.Setup(repo => repo.Translators).Returns(translators.AsQueryable());

            // Act
            var result = controller.GetTranslatorsByName(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTranslators = Assert.IsAssignableFrom<Translator[]>(okResult.Value);
            returnedTranslators.Should().HaveCount(2);
            returnedTranslators.Should().BeEquivalentTo(translators);
        }

        [Fact]
        public async Task AddTranslator_ValidTranslator_ReturnsOkWithCreatedTranslator()
        {
            // Arrange
            var translator = new Translator { Name = "John" };
            mockRepository.Setup(repo => repo.CreateTranslatorAsync(translator)).ReturnsAsync(true);

            // Act
            var result = await controller.AddTranslator(translator);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTranslator = Assert.IsAssignableFrom<bool>(okResult.Value);
            returnedTranslator.Should().Be(true);
        }

        [Fact]
        public async Task UpdateTranslatorStatus_ValidStatus_ReturnsOkWithUpdatedStatus()
        {
            // Arrange
            var newStatus = "Certified";
            var translatorId = 1;

            // Act
            var result = await controller.UpdateTranslatorStatus(newStatus, translatorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("updated");
            mockRepository.Verify(repo => repo.UpdateTranslatorStatusAsync(translatorId, newStatus), Times.Once);
        }

        [Theory]
        [InlineData("InvalidStatus")]
        public async Task UpdateTranslatorStatus_InvalidStatus_ReturnsBadRequest(string newStatus)
        {
            // Arrange
            var translatorId = 1;

            // Act
            var result = await controller.UpdateTranslatorStatus(newStatus, translatorId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("unknown status");
            mockRepository.Verify(repo => repo.UpdateTranslatorStatusAsync(translatorId, It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("Applicant")]
        [InlineData("Certified")]
        [InlineData("Deleted")]
        public void IsValidTranslatorStatus_ValidStatus_ReturnsTrue(string status)
        {
            // Arrange

            // Act
            var result = TranslatorManagementController.IsValidTranslatorStatus(status);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("InvalidStatus")]
        public void IsValidTranslatorStatus_InvalidStatus_ReturnsFalse(string status)
        {
            // Arrange

            // Act
            var result = TranslatorManagementController.IsValidTranslatorStatus(status);

            // Assert
            result.Should().BeFalse();
        }
    }
}
