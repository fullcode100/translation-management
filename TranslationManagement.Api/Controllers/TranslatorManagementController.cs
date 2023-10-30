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
        public Translator[] GetTranslatorsByName([FromBody] string? name)
        {
            if (name != null)
            {
                return repository.Translators.Where(t => t.Name == name).ToArray();
            }
            return repository.Translators.ToArray();
        }

        [HttpPost]
        public bool AddTranslator(Translator translator)
        {
            return repository.CreateTranslator(translator);
        }

        [HttpPut("{translatorId}")]
        public string UpdateTranslatorStatus([FromBody] string newStatus, int translatorId)
        {
            _logger.LogInformation($"User status update request: {newStatus} for user {translatorId.ToString()}");
            if (!IsValidTranslatorStatus(newStatus))
            {
                throw new ArgumentException("unknown status");
            }

            repository.UpdateTranslatorStatus(translatorId, newStatus);

            return "updated";
        }

        public static bool IsValidTranslatorStatus(string status)
        {
            return Enum.IsDefined(typeof(TranslatorStatuses), status);
        }
    }
}
