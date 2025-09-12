using Microsoft.AspNetCore.Mvc;

namespace RendezVous.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RendezVousController : ControllerBase
    {
        private static readonly string[] Notes = new[]
        {
            "Driving", "Doctor", "Beauty", "Baby"
        };

        private readonly ILogger<RendezVousController> _logger;

        public RendezVousController(ILogger<RendezVousController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRendezVous")]
        public IEnumerable<RendezVous> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new RendezVous
            {
                Date = DateTime.Now.AddDays(index),
                Subject = "Subject"+index,
                Notes = Notes[Random.Shared.Next(Notes.Length)]
            })
            .ToArray();
        }
    }
}
