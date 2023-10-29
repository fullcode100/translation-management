using Microsoft.EntityFrameworkCore;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.Controllers;

namespace TranslationManagement.Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TranslationJobController.TranslationJob> TranslationJobs { get; set; }
        public DbSet<TranslatorManagementController.TranslatorModel> Translators { get; set; }
    }
}