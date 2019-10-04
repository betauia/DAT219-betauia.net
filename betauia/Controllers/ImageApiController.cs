using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;

namespace betauia.Controllers
{
  [ApiController]
  [Route("api/image")]
  public class ImageApiController : ControllerBase
  {
    private readonly ApplicationDbContext _dbContext;

    public ImageApiController(ApplicationDbContext db)
    {
      _dbContext = db;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(string id)
    {
      var imageModel = _dbContext.Images.Find(id);
      var image = System.IO.File.OpenRead(imageModel.Location);
      return File(image, "image/" + imageModel.ImageType);
    }

    [HttpPost]
    public async Task<IActionResult> PostImage([FromForm] ImageModelView imageModelView)
    {
      var image = imageModelView.Image;
      if (image.Length > 0)
      {
        var contentType = image.ContentType.Split("/");
        if (contentType[0] != "image") return BadRequest("not an image");

        var type = contentType[1];
        string id = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

        var name = id + "." + type;
        var filepath = "wwwroot/Uploads/" +name;

        using (var fileStream = new FileStream(filepath, FileMode.Create))
        {
          image.CopyTo(fileStream);
        }

        var dbimage = new ImageModel(id,filepath,type);
        _dbContext.Images.Add(dbimage);
        _dbContext.SaveChanges();

        return Ok(id);
      }
      return BadRequest();
    }
  }
}
