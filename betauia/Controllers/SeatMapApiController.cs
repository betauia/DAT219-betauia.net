using System.Linq;
using System.Net.Mime;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;


namespace betauia.Controllers
{
    [Route("api/seatmap")]
    [ApiController]
    public class SeatMapApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public SeatMapApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllSeats()
        {
            return Ok(_context.Seats.ToList());
        }

        [HttpGet("id")]
        public IActionResult GetSeat(int id)
        {
            var SeatMapModel = _context.Seats.Find(id);

            if (SeatMapModel == null)
                return NotFound();

            return Ok(SeatMapModel);
        }

        [HttpPost]
        public IActionResult PostSeatMap(SeatMapModel seatMapModel)
        {
            const int numSeats = 20;
            
            if (seatMapModel.Id != 0)
                return BadRequest();
            
            
            var seatMap = new SeatMapModel(numSeats, 100);
            
            for (var i = 1; i <= numSeats; i++)
            {
                var seat = new SeatModel(i) {Owner = seatMap};
                _context.Add(seat);
            }
            
            _context.Add(seatMap);

            _context.SaveChanges();
            return Created("Ok", seatMapModel);
        }
    }
}