using Microsoft.EntityFrameworkCore;
using TranslationManagement.Api.Controllers;

namespace TranslationManagement.Api.Models
{
  public class EFDataRepository : IDataRepository
  {
    private readonly AppDbContext _dbContext;

    public EFDataRepository(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IQueryable<TranslationJob> TranslationJobs => _dbContext.TranslationJobs;
    public IQueryable<Translator> Translators => _dbContext.Translators;

    public async Task<bool> CreateTranslationJob(TranslationJob job)
    {
      await _dbContext.TranslationJobs.AddAsync(job);
      var result = await _dbContext.SaveChangesAsync();
      return result > 0;
    }

    public async Task<bool> CreateTranslator(Translator translator)
    {
      await _dbContext.Translators.AddAsync(translator);
      var result = _dbContext.SaveChanges();
      return result > 0;
    }

    public async Task UpdateTranslationJobStatus(int jobId, string newStatus)
    {
      var job = await _dbContext.TranslationJobs.SingleAsync(j => j.Id == jobId);
      if (job == null)
      {
        throw new Exception("job does not exist");
      }

      if (TranslationJobController.IsInvalidStatusChange(job.Status, newStatus))
      {
        throw new Exception("invalid status change");
      }

      job.Status = newStatus;
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTranslatorStatus(int translatorId, string newStatus)
    {
      var job = await _dbContext.Translators.SingleAsync(j => j.Id == translatorId);
      if (job == null)
      {
        throw new Exception("job does not exist");
      }

      job.Status = newStatus;
      await _dbContext.SaveChangesAsync();
    }
  }
}
