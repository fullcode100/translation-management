using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationManagement.Api.Models
{
  public class TranslationJob
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
