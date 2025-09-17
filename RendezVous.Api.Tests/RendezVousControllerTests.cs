using Moq;
using Microsoft.Extensions.Logging;
using RendezVous.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace RendezVous.Api.Tests
{


    public class RendezVousControllerTests
    {
        [Fact]
        public void Get_ReturnsListOfRendezVous()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RendezVousController>>();
            var controller = new RendezVousController(loggerMock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var rendezVousList = Assert.IsAssignableFrom<IEnumerable<RendezVous>>(okResult.Value);
            Assert.Equal(5, rendezVousList.Count());
        }
    }
}