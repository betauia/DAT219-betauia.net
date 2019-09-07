namespace betauia.Controllers
{
  public class EventAtendee
  {
    public EventAtendee()
    {
      Userid = null;
      Email = null;
      Firstname = null;
      Lastname = null;
    }
    public int ID { get; set; }
    public int EventId { get; set; }
    public string Userid { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
  }
}
