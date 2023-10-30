namespace TranslationManagement.Api.Models
{
  public interface IDataRepository
  {
    public IQueryable<TranslationJob> TranslationJobs { get; }
    public IQueryable<Translator> Translators { get; }

    public bool CreateTranslationJob(TranslationJob job);
    public bool CreateTranslator(Translator translator);
    public void UpdateTranslationJobStatus(int jobId, string newStatus);
    public void UpdateTranslatorStatus(int translatorId, string newStatus);
  }
}
