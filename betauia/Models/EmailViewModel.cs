namespace betauia.Models
{
  public class EmailViewModel
  {
    public string ButtonLink { get; set; }

    public EmailViewModel(string buttonLink)
    {
      ButtonLink = buttonLink;
    }
  }
}
