namespace TranslationManagement.Api.Models
{
  public class TranslationJob
  {
    public int Id { get; set; }
    public string CustomerName { get; set; } = "";
    public string Status { get; set; } = JobStatuses.New.ToString();
    public string OriginalContent { get; set; } = "";
    public string TranslatedContent { get; set; } = "";
    public double Price { get; set; }
  }

  public enum JobStatuses
  {
    New,
    InProgress,
    Completed
  }
}
