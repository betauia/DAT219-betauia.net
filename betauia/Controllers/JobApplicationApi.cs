using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
  [ApiController]
  [Route("api/jobs")]

  public class JobApplicationApi : ControllerBase
  {
    private readonly ApplicationDbContext _dbContext;

    public JobApplicationApi(ApplicationDbContext db)
    {
      _dbContext = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
      return Ok(_dbContext.JobApplications.ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(int id)
    {
      var job = _dbContext.JobApplications.Find(id);
      if (job == null)
      {
        return BadRequest();
      }
      return Ok(job);
    }

    [HttpPost]
    public async Task<IActionResult> PostJob(JobApplication job)
    {
      _dbContext.JobApplications.Add(job);
      _dbContext.SaveChanges();
      return Ok(job);
    }

    [HttpPut("{id}")]
    public IActionResult EditJob(int id, JobApplication jobModel)
    {
      if (id != jobModel.Id)
      {
        return BadRequest();
      }

      var job = _dbContext.JobApplications.Find(id);
      job.Content = jobModel.Content;
      job.Title = jobModel.Title;
      job.Url = jobModel.Url;
      _dbContext.JobApplications.Update(job);
      _dbContext.SaveChanges();
      return Ok(job);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
      var job = _dbContext.JobApplications.Find(id);
      if (job == null) return BadRequest();

      job.Title = null;
      job.Content = null;
      job.Url = null;

      _dbContext.JobApplications.Update(job);
      _dbContext.SaveChanges();
      return Ok(job);
    }
  }
}
