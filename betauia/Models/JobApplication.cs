using System.ComponentModel.DataAnnotations;

namespace betauia.Models
{
  public class JobApplication
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public string Url { get; set; }
  }
}
