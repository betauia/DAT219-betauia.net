using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventApiController(ApplicationDbContext context)
                 {
            // Set the dbcontext
            _context = context;
        }

        // Tested and working
        // GET: Get all events
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return with 200 OK status code
            var events = _context.Events.ToList();
            var eventViews = new List<EventCreater>();
            foreach (var eventModel in events)
            {
              var seatmap = _context.EventSeatMaps.Find(eventModel.SeatMapId);
              if (seatmap != null) eventModel.SeatMap = seatmap;

              var eventSponsors = _context.EventSponsors.Where(a => a.EventId == eventModel.Id).ToList();
              var sponsors = new List<SponsorModel>();
              foreach (var eventSponsor in eventSponsors)
              {
                sponsors.Add(_context.Sponsors.Find(eventSponsor.SponsorId));
              }

              var eventView = new EventCreater();
              eventView.eventModel = eventModel;
              eventView.sponsors = sponsors;
              eventViews.Add(eventView);
            }
            return Ok(eventViews);
        }

        // Tested and working
        // GET: Get event by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventModel(int id)
        {
            // Get user by id
            var eventModel = _context.Events.Find(id);

            // Check if user is valid
            if (eventModel == null)
                return NotFound();

            var eventSponsors = _context.EventSponsors.Where(a => a.EventId == eventModel.Id).ToList();
            var sponsors = new List<SponsorModel>();
            foreach (var eventSponsor in eventSponsors)
            {
              sponsors.Add(_context.Sponsors.Find(eventSponsor.SponsorId));
            }

            var eventView = new EventCreater();
            eventView.eventModel = eventModel;
            eventView.sponsors = sponsors;
            // Return user
            return Ok(eventView);
        }

        // Tested and working
        // PUT: Update event by id
        [Authorize("Event.write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventModel(int id, EventEditor eventEditor)
        {
          var eventModel = eventEditor.eventModel;
          if (id != eventModel.Id) return BadRequest("id not same");
          var Event = _context.Events.Find(eventModel.Id);
          if (Event == null) return NotFound();

          //Set valuse
          Event.Title = eventModel.Title;
          Event.Description = eventModel.Description;
          Event.Content = eventModel.Content;
          Event.IsPublic = eventModel.IsPublic;
          Event.Enddate = eventModel.Enddate;
          Event.Startdate = eventModel.Startdate;
          Event.Image = null;
          if (eventModel.Image != null)
          {
            Event.Image = eventModel.Image;
          }

          //Set sponsors
          var sponsors = _context.EventSponsors.Where(a => a.EventId == id);
          _context.EventSponsors.RemoveRange(sponsors);

          foreach (var sponsor in eventEditor.sponsors)
          {
            if (!_context.EventSponsors.Any(p => p.SponsorId == sponsor && p.EventId == Event.Id))
            {
              var eventSponsor = new EventSponsor
              {
                EventId = Event.Id,
                SponsorId = sponsor
              };
              _context.EventSponsors.Add(eventSponsor);
            }
          }
          _context.Update(Event);
          _context.SaveChanges();
          return Ok(Event);
        }

        // Tested and working
        // POST: Add new event
        [Authorize("Event.write")]
        [HttpPost]
        public async Task<IActionResult> Post(EventCreater eventCreater)
        {
            // Return if id is set to avoid overwriting an existing event
            if (eventCreater.eventModel.Id != 0) return BadRequest();

            var tseatmapid = eventCreater.eventModel.SeatMapId;
            eventCreater.eventModel.SeatMapId = null;

            _context.Add(eventCreater.eventModel);
            _context.SaveChanges();

            if (tseatmapid != null)
            {
                var tSeatmap = _context.SeatMaps.Find(tseatmapid);
                var seatmap = CreateSeatMap(eventCreater.eventModel, tSeatmap);

                var tseats = _context.Seats.Where(r => r.OwnerId == tSeatmap.Id).ToList();
                var seats = CreateSeats(seatmap, tseats);
                _context.EventSeatMaps.Add(seatmap);
                _context.EventSeats.AddRange(seats);
                eventCreater.eventModel.SeatMapId = seatmap.Id;
                eventCreater.eventModel.SeatMap = seatmap;
            }

            foreach (var sponsorModel in eventCreater.sponsors)
            {
              var eventsponsor = new EventSponsor
              {
                EventId = eventCreater.eventModel.Id,
                SponsorId = sponsorModel.Id,
              };
              _context.EventSponsors.Add(eventsponsor);
            }

            // Add and save
            _context.Update(eventCreater.eventModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEventModel), new {id = eventCreater.eventModel.Id}, eventCreater.eventModel);
        }

        // Tested and working
        // DELETE: Delete event by id
        [Authorize("Event.write")]
        [HttpDelete("{id}")]
        public IActionResult DeleteEventModel(int id)
        {
            // Receive and check if event is valid
            var eventModel = _context.Events.Find(id);
            if (eventModel == null) return NotFound();

            eventModel.Atendees = 0;
            eventModel.Author = null;
            eventModel.Content = null;
            eventModel.Description = null;
            eventModel.Image = null;
            eventModel.EventPrice = 0;
            eventModel.Startdate = null;
            eventModel.Enddate = null;
            eventModel.IsPublic = false;
            eventModel.MaxAtendees = 0;
            eventModel.SubTitle = null;

            _context.Update(eventModel);
            _context.SaveChanges();
            return Ok(eventModel);
        }

        // Function to check if an event by id exists
        private bool EventModelExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private EventSeatMap CreateSeatMap(EventModel eventModel, SeatMapModel seatMapModel)
        {
            var seatmap = new EventSeatMap
            {
                NumSeats = seatMapModel.NumSeats,
                NumSeatsAvailable = seatMapModel.NumSeats,
                EventId = eventModel.Id,
                Id = eventModel.Id + seatMapModel.Id,
            };
            return seatmap;
        }

        private List<EventSeat> CreateSeats(EventSeatMap seatMap, List<SeatModel> tseats)
        {
            List<EventSeat> seats = new List<EventSeat>();
            foreach (var tseat in tseats)
            {
                var seat = new EventSeat
                {
                    Number = tseat.Number,
                    x = tseat.x,
                    y = tseat.y,
                    OwnerId = seatMap.Id,
                    Id = seatMap.Id + tseat.Number,
                };
                seats.Add(seat);
            }
            return seats;
        }

        public class EventCreater
        {
          public EventModel eventModel { get; set; }
          public List<SponsorModel> sponsors { get; set; }
        }

        public class EventEditor
        {
          public EventModel eventModel { get; set; }
          public List<string> sponsors { get; set; }
        }
    }

}
