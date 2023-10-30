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

    public bool CreateTranslationJob(TranslationJob job)
    {
      _dbContext.TranslationJobs.Add(job);
      return _dbContext.SaveChanges() > 0;
    }

    public bool CreateTranslator(Translator translator)
    {
      _dbContext.Translators.Add(translator);
      return _dbContext.SaveChanges() > 0;
    }

    public void UpdateTranslationJobStatus(int jobId, string newStatus)
    {
      var job = _dbContext.TranslationJobs.Single(j => j.Id == jobId);
      if (job == null)
      {
        throw new Exception("job does not exist");
      }

      if (TranslationJobController.IsInvalidStatusChange(job.Status, newStatus))
      {
        throw new Exception("invalid status change");
      }

      job.Status = newStatus;
      _dbContext.SaveChanges();
    }

    public void UpdateTranslatorStatus(int translatorId, string newStatus)
    {
      var job = _dbContext.Translators.Single(j => j.Id == translatorId);
      if (job == null)
      {
        throw new Exception("job does not exist");
      }

      job.Status = newStatus;
      _dbContext.SaveChanges();
    }
  }
}
