using System.Linq;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
  [ApiController]
  [Route("api/image")]
  public class ImageController : ControllerBase
  {
    private readonly ApplicationDbContext _dbContext;

    public ImageController(ApplicationDbContext db)
    {
      _dbContext = db;
    }

    [HttpGet]
    public IActionResult GetAllImages()
    {
      return Ok(_dbContext.Images.ToList());
    }

    [HttpPost]
    public IActionResult PostImage(ImageModel imageModel)
    {
      _dbContext.Images.Add(imageModel);
      _dbContext.SaveChanges();
      return Ok(imageModel);
    }
  }
}
