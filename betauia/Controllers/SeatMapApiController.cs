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
            if (seatMapModel.Id != 0)
                return BadRequest();

            _context.Add(seatMapModel);
            _context.SaveChanges();
            return Created("Ok", seatMapModel);
        }
    }
}