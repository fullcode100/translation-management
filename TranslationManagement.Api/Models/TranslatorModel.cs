using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslationManagement.Api.Models
{
  public class TranslatorModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string HourlyRate { get; set; } = "";
    public string Status { get; set; } = TranslatorStatuses.Applicant.ToString();
    public string CreditCardNumber { get; set; } = "";
  }

  public enum TranslatorStatuses
  {
    Applicant,
    Certified,
    Deleted
  }
}
