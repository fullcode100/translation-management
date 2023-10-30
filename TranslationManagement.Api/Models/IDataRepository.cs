namespace TranslationManagement.Api.Models
{
  public interface IDataRepository
  {
    public IQueryable<TranslationJob> TranslationJobs { get; }
    public IQueryable<Translator> Translators { get; }

    public Task<bool> CreateTranslationJobAsync(TranslationJob job);
    public Task<bool> CreateTranslatorAsync(Translator translator);
    public Task UpdateTranslationJobStatusAsync(int jobId, string newStatus);
    public Task UpdateTranslatorStatusAsync(int translatorId, string newStatus);
  }
}
