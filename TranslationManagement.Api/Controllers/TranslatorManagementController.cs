using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
    public class TranslatorManagementController : ControllerBase
    {
        public class TranslatorModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string HourlyRate { get; set; }
            public string Status { get; set; }
            public string CreditCardNumber { get; set; }
        }

        public static readonly string[] TranslatorStatuses = { "Applicant", "Certified", "Deleted" };

        private readonly ILogger<TranslatorManagementController> _logger;
        private AppDbContext _context;

        public TranslatorManagementController(IServiceScopeFactory scopeFactory, ILogger<TranslatorManagementController> logger)
        {
            _context = scopeFactory.CreateScope().ServiceProvider.GetService<AppDbContext>();
            _logger = logger;
        }

        [HttpGet]
        public TranslatorModel[] GetTranslators()
        {
            return _context.Translators.ToArray();
        }

        [HttpGet]
        public TranslatorModel[] GetTranslatorsByName(string name)
        {
            return _context.Translators.Where(t => t.Name == name).ToArray();
        }

        [HttpPost]
        public bool AddTranslator(TranslatorModel translator)
        {
            _context.Translators.Add(translator);
            return _context.SaveChanges() > 0;
        }
        
        [HttpPost]
        public string UpdateTranslatorStatus(int Translator, string newStatus = "")
        {
            _logger.LogInformation("User status update request: " + newStatus + " for user " + Translator.ToString());
            if (TranslatorStatuses.Where(status => status == newStatus).Count() == 0)
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