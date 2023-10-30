using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/translators")]
    public class TranslatorManagementController : ControllerBase
    {
        private readonly IDataRepository repository;
        private readonly ILogger<TranslatorManagementController> _logger;

        public TranslatorManagementController(IDataRepository repo, ILogger<TranslatorManagementController> logger)
        {
            repository = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTranslatorsByName([FromBody] string? name)
        {
            if (name != null)
            {
                return Ok(repository.Translators.Where(t => t.Name == name).ToArray());
            }
            return Ok(repository.Translators.ToArray());
        }

        [HttpPost]
        public IActionResult AddTranslator(Translator translator)
        {
            return Ok(repository.CreateTranslator(translator));
        }

        [HttpPut("{translatorId}")]
        public IActionResult UpdateTranslatorStatus([FromBody] string newStatus, int translatorId)
        {
            _logger.LogInformation($"User status update request: {newStatus} for user {translatorId.ToString()}");
            if (!IsValidTranslatorStatus(newStatus))
            {
                return BadRequest("unknown status");
            }

            try
            {
                repository.UpdateTranslatorStatus(translatorId, newStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("updated");
        }

        public static bool IsValidTranslatorStatus(string status)
        {
            return Enum.IsDefined(typeof(TranslatorStatuses), status);
        }
    }
}
