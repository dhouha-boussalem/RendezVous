using Moq;
using Microsoft.Extensions.Logging;
using RendezVous.Controllers;
using Microsoft.AspNetCore.Mvc;
using RendezVous.Services;
using RendezVous;
using System.Collections.Generic;

namespace RendezVous.Api.Tests
{


    public class RendezVousControllerTests
    {
        [Fact]
        public void Get_ReturnsListOfRendezVous()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RendezVousController>>();
            var service = new RendezVousService();
            var controller = new RendezVousController(loggerMock.Object, service);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var rendezVousList = Assert.IsAssignableFrom<IEnumerable<RendezVous>>(okResult.Value);
            Assert.Empty(rendezVousList); // La liste mémoire est vide au départ
        }

        [Fact]
        public void Add_ValidRendezVous_ReturnsCreated()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RendezVousController>>();
            var service = new RendezVousService();
            var controller = new RendezVousController(loggerMock.Object, service);
            var rendezVous = new RendezVous
            {
                Date = DateTime.Now.AddDays(1),
                Subject = "Test Subject",
                Notes = "Test Notes",
                Location = "Paris"
            };

            // Act
            var result = controller.Add(rendezVous);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var returnedRendezVous = Assert.IsType<RendezVous>(createdResult.Value);
            Assert.Equal(rendezVous.Subject, returnedRendezVous.Subject);
            Assert.Equal(rendezVous.Notes, returnedRendezVous.Notes);
            Assert.Equal(rendezVous.Location, returnedRendezVous.Location);
        }

        [Fact]
        public void Add_NullRendezVous_ReturnsBadRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RendezVousController>>();
            var service = new RendezVousService();
            var controller = new RendezVousController(loggerMock.Object, service);

            // Act
            var result = controller.Add(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Le rendez-vous ne peut pas être nul.", badRequestResult.Value);
        }
    }
}