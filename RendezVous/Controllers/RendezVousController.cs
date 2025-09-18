using Microsoft.AspNetCore.Mvc;
using RendezVous.Services;

namespace RendezVous.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des rendez-vous.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RendezVousController : ControllerBase
    {
        private readonly ILogger<RendezVousController> _logger;
        private readonly IRendezVousService _rendezVousService;

        public RendezVousController(ILogger<RendezVousController> logger, IRendezVousService rendezVousService)
        {
            _logger = logger;
            _rendezVousService = rendezVousService;
        }

        /// <summary>
        /// Récupère une liste de rendez-vous.
        /// </summary>
        [HttpGet(Name = "GetRendezVous")]
        [ProducesResponseType(typeof(IEnumerable<RendezVous>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RendezVous>> Get()
        {
            try
            {
                var rendezVousList = _rendezVousService.GetAll();
                return Ok(rendezVousList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des rendez-vous.");
                return StatusCode(500, "Une erreur interne est survenue.");
            }
        }

        /// <summary>
        /// Ajoute un nouveau rendez-vous.
        /// </summary>
        [HttpPost(Name = "AddRendezVous")]
        [ProducesResponseType(typeof(RendezVous), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RendezVous> Add([FromBody] RendezVous rendezVous)
        {
            if (rendezVous == null)
            {
                return BadRequest("Le rendez-vous ne peut pas être nul.");
            }
            _rendezVousService.Add(rendezVous);
            return CreatedAtRoute("GetRendezVous", new { }, rendezVous);
        }
    }
}