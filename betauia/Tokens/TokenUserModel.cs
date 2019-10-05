using System.ComponentModel.DataAnnotations;

namespace betauia.Tokens
{
  public class TokenUserModel
  {
    [Key]
    public string User { get; set; }
    public string Token { get; set; }
  }
}
