using System;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace betauia.Controllers
{
  [Route("api/calendar")]
  public class CalendarApiController : ControllerBase
  {
    static string[] Scopes = { CalendarService.Scope.Calendar };
    static string ApplicationName = "Google Calendar API .NET Quickstart";


    [HttpGet]
    public IActionResult Test()
    {
      UserCredential credential;
      using (var stream =
        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
      {
        // The file token.json stores the user's access and refresh tokens, and is created
        // automatically when the authorization flow completes for the first time.
        string credPath = "token.json";
        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
          GoogleClientSecrets.Load(stream).Secrets,
          Scopes,
          "user",
          CancellationToken.None,
          new FileDataStore(credPath, true)).Result;
        Console.WriteLine("Credential file saved to: " + credPath);
      }

      var service = new CalendarService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = credential,
        ApplicationName = ApplicationName,
      });

      Event googleEvent = new Event
      {
        Summary = "Google test",
        Location = "Jon Lilletuns vei 9, 4879 Grimstad",
        Description = "Pills",
        Start = new EventDateTime()
        {
          DateTime = DateTime.Now.AddDays(1),
          TimeZone = "America/Los_Angeles",
        },
        End = new EventDateTime()
        {
          DateTime = DateTime.Now.AddDays(2),
          TimeZone = "America/Los_Angeles",
        }
      };
      String calenderId = "erikaspen1@gmail.com";
      EventsResource.InsertRequest request = service.Events.Insert(googleEvent, calenderId);
      Event createdEvent = request.Execute();

      EventsResource.ListRequest lrequest = service.Events.List("primary");
       lrequest.TimeMin = DateTime.Now;
       lrequest.ShowDeleted = false;
       lrequest.SingleEvents = true;
       lrequest.MaxResults = 10;
       lrequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

       // List events.
       Events events = lrequest.Execute();
       events.Items.Where(a => a.Id == createdEvent.Id);

       EventsResource er = new EventsResource(service);
       var queryEvent = er.List(calenderId);
       var EventList = queryEvent.Execute();
       var gevent = EventList.Items.Where(a => a.Id == createdEvent.Id).First();
       gevent.Summary = "another summary";
       gevent.Description = "2 pills";
       er.Update(gevent, calenderId, createdEvent.Id).Execute();
       return Ok(createdEvent.HtmlLink + "    " + createdEvent.Id);
    }
  }
}
