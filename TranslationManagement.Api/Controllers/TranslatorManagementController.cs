using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
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
        public TranslatorModel[] GetTranslators() => _context.Translators.ToArray();

        [HttpGet]
        public TranslatorModel[] GetTranslatorsByName(string name) => _context.Translators.Where(t => t.Name == name).ToArray();

        [HttpPost]
        public bool AddTranslator(TranslatorModel translator)
        {
            _context.Translators.Add(translator);
            return _context.SaveChanges() > 0;
        }

        [HttpPost]
        public string UpdateTranslatorStatus(int Translator, string newStatus = "")
        {
            _logger.LogInformation($"User status update request: {newStatus} for user {Translator.ToString()}");
            if (!Enum.IsDefined(typeof(TranslatorStatuses), newStatus))
            {
                throw new ArgumentException("unknown status");
            }

            var job = _context.Translators.Single(j => j.Id == Translator);
            job.Status = newStatus;
            _context.SaveChanges();

            return "updated";
        }
    }
}