using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using betauia.Data;
using betauia.FileReader;
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
    private readonly IimageIO _imageIo;

    public ImageApiController(ApplicationDbContext db,IimageIO imageIo)
    {
      _dbContext = db;
      _imageIo = imageIo;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(string id)
    {
      var imageModel = await _dbContext.Images.FindAsync(id);
      var image = await _imageIo.GetFile(imageModel.Location);
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
        await _imageIo.WriteFile(filepath,image);
        var dbimage = new ImageModel(id,filepath,type);
        await _dbContext.Images.AddAsync(dbimage);
        await _dbContext.SaveChangesAsync();

        return Ok(id);
      }
      return BadRequest();
    }
  }
}
