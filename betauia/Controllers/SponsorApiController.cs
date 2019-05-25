using System.Linq;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [ApiController]
    [Route("api/sponsor")]
    public class SponsorApicController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SponsorApicController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllSponser()
        {
            return Ok(_db.Sponsors.ToList());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetSponser(string id)
        {
            var sponsor = _db.Sponsors.Find(id);
            if (sponsor == null) return NotFound();
            return Ok(sponsor);
        }

        [HttpPost]
        public IActionResult AddSponsor(SponsorModel sponsorModel)
        {
            if (_db.Sponsors.Find(sponsorModel.Id) != null) return BadRequest("sponsor id taken");
            _db.Sponsors.Add(sponsorModel);
            _db.SaveChanges();
            return Ok(sponsorModel);
        }

        [HttpPut("{id}")]
        public IActionResult EditSponsor(string id, SponsorModel sponsorModel)
        {
            if (_db.Sponsors.Find(sponsorModel.Id) != null) return BadRequest("sponsor id taken");
            
            var sponsor = _db.Sponsors.Find(id);

            if (sponsorModel.Id != "" || sponsorModel.Id != null)
            {
                sponsor.Id = sponsorModel.Id;
            }
            
            sponsor.Description = sponsorModel.Description;
            sponsor.Title = sponsorModel.Title;
            sponsor.Url = sponsorModel.Url;

            _db.Update(sponsor);
            _db.SaveChanges();
            return Ok(sponsorModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSponsor(string id)
        {
            var sponsor = _db.Sponsors.Find(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            sponsor.Description = null;
            sponsor.Title = null;
            sponsor.Url = null;
            
            _db.Update(sponsor);
            _db.SaveChanges();
            return Ok();
        }
    }
}