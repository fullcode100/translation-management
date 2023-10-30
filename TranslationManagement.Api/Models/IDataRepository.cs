namespace TranslationManagement.Api.Models
{
  public interface IDataRepository
  {
    public IQueryable<TranslationJob> TranslationJobs { get; }
    public IQueryable<Translator> Translators { get; }

    public Task<bool> CreateTranslationJob(TranslationJob job);
    public Task<bool> CreateTranslator(Translator translator);
    public Task UpdateTranslationJobStatus(int jobId, string newStatus);
    public Task UpdateTranslatorStatus(int translatorId, string newStatus);
  }
}
