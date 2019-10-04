using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetSponser(string id)
        {
            var sponsor = await _db.Sponsors.FindAsync(id);
            if (sponsor == null) return NotFound();
            return Ok(sponsor);
        }

        [Authorize("Sponsor.write")]
        [HttpPost]
        public async Task<IActionResult> AddSponsor(SponsorModel sponsorModel)
        {
          sponsorModel.Id = sponsorModel.Title.ToLower().Replace(" ","");
          if (await _db.Sponsors.FindAsync(sponsorModel.Id) != null) return BadRequest();

          await _db.Sponsors.AddAsync(sponsorModel);
          await _db.SaveChangesAsync();
          return Ok(sponsorModel);
        }

        [Authorize("Sponsor.write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSponsor(string id, SponsorModel sponsorModel)
        {
            if (id != sponsorModel.Id)
            {
                return BadRequest();
            }

            var sponsor = await _db.Sponsors.FindAsync(id);

            sponsor.Description = sponsorModel.Description;
            sponsor.Title = sponsorModel.Title;
            sponsor.Url = sponsorModel.Url;

            _db.Update(sponsor);
            await _db.SaveChangesAsync();
            return Ok(sponsor);
        }

        [Authorize("Sponsor.write")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor(string id)
        {
            var sponsor = await _db.Sponsors.FindAsync(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            sponsor.Description = null;
            sponsor.Title = null;
            sponsor.Url = null;

            _db.Update(sponsor);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
