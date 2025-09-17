using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RendezVous.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des rendez-vous.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RendezVousController : ControllerBase
    {
        private static readonly string[] Notes = new[]
        {
            "Driving", "Doctor", "Beauty", "Baby"
        };

        private static readonly string[] Locations = new[]
        {
            "Paris", "Lyon", "Marseille", "Toulouse", "Nice"
        };

        private readonly ILogger<RendezVousController> _logger;
        private readonly Random _random;

        public RendezVousController(ILogger<RendezVousController> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        /// <summary>
        /// Récupère une liste de rendez-vous fictifs.
        /// </summary>
        [HttpGet(Name = "GetRendezVous")]
        [ProducesResponseType(typeof(IEnumerable<RendezVous>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RendezVous>> Get()
        {
            try
            {
                var rendezVousList = Enumerable.Range(1, 5).Select(index => new RendezVous
                {
                    Date = DateTime.Now.AddDays(index),
                    Subject = $"Subject{index}",
                    Notes = Notes[_random.Next(Notes.Length)],
                    Location = Locations[_random.Next(Locations.Length)]
                })
                .ToArray();

                return Ok(rendezVousList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération des rendez-vous.");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }
    }
}