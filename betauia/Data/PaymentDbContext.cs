using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using betauia.Models;

namespace betauia.Data
{
  public class PaymentDbContext : IdentityDbContext
  {
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
      : base(options)
    {

    }


  }
}
