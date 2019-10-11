using System.Configuration;
using System.Threading.Tasks;
using betauia.Controllers;
using betauia.Data;
using betauia.Models;
using Microsoft.EntityFrameworkCore;

namespace betauia.Ticket
{
    public class DeleteTicket
    {
        public async static Task Delete(int id,long delay)
        {
            while (delay > 0)
            {
                var currentDelay = delay > int.MaxValue ? int.MaxValue : (int) delay;
                await Task.Delay(currentDelay);
                delay -= currentDelay;
            }

            var connectionstring = "server=beta_db;port=3306;database=db;user=user;password=hackmeuser";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(connectionstring);
            var db = new ApplicationDbContext(optionsBuilder.Options);
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket.Status == "STARTED" || ticket.Status == "INITIATE")
            {
                ticket.CancelTicket(db);
                ticket.Status = "CANCEL";
                db.SaveChanges();
            }
        }
    }
}
