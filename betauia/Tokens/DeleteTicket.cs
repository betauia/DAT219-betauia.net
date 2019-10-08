using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;

namespace betauia.Tokens
{
    public class DeleteTicket
    {
        public async static Task Delete(ApplicationDbContext dbContext, int id,long delay)
        {
            while (delay > 0)
            {
                var currentDelay = delay > int.MaxValue ? int.MaxValue : (int) delay;
                await Task.Delay(currentDelay);
                delay -= currentDelay;
            }
            var ticket = dbContext.Tickets.Find(id);
            if (ticket.Status == "STARTED")
            {
                ticket.CancelTicket(dbContext);
                ticket.Status = "CANCEL";
                dbContext.SaveChanges();
            }
        }
        
        public async static Task CancelTransaction(ApplicationDbContext dbContext, int id,long delay)
        {
            while (delay > 0)
            {
                var currentDelay = delay > int.MaxValue ? int.MaxValue : (int) delay;
                await Task.Delay(currentDelay);
                delay -= currentDelay;
            }

            var ticket = dbContext.Tickets.Find(id);
            if (ticket.Status == "INITIATE")
            {
                ticket.CancelTicket(dbContext);
                ticket.Status = "CANCEL";
                dbContext.SaveChanges();
            }
        }
    }
}