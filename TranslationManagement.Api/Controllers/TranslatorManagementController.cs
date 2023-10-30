using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/translators")]
    public class TranslatorManagementController : ControllerBase
    {
        private readonly ILogger<TranslatorManagementController> _logger;
        private AppDbContext _context;

        public TranslatorManagementController(IServiceScopeFactory scopeFactory, ILogger<TranslatorManagementController> logger)
        {
            var context = scopeFactory.CreateScope().ServiceProvider.GetService<AppDbContext>();
            _context = context ?? throw new ArgumentNullException("AppDbContext service cannot be null");
            _logger = logger;
        }

        [HttpGet]
        public TranslatorModel[] GetTranslatorsByName([FromBody] string? name)
        {
            if (name != null)
            {
                return _context.Translators.Where(t => t.Name == name).ToArray();
            }
            return _context.Translators.ToArray();
        }

        [HttpPost]
        public bool AddTranslator(TranslatorModel translator)
        {
            _context.Translators.Add(translator);
            return _context.SaveChanges() > 0;
        }

        [HttpPut("{translator}")]
        public string UpdateTranslatorStatus([FromBody] string newStatus, int translator)
        {
            _logger.LogInformation($"User status update request: {newStatus} for user {translator.ToString()}");
            if (!IsValidTranslatorStatus(newStatus))
            {
                throw new ArgumentException("unknown status");
            }

            var job = _context.Translators.Single(j => j.Id == translator);
            job.Status = newStatus;
            _context.SaveChanges();

            return "updated";
        }

        private bool IsValidTranslatorStatus(string status)
        {
            return Enum.IsDefined(typeof(TranslatorStatuses), status);
        }
    }
}
