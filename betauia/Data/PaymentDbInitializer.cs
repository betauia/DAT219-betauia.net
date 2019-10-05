namespace betauia.Data
{
  public class PaymentDbInitializer
  {
    public static void Initialize(PaymentDbContext db)
    {
      db.Database.EnsureDeleted();
      db.Database.EnsureCreated();
    }
  }
}
