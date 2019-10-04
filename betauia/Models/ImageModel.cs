using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace betauia.Models
{
  public class ImageModel
  {
    public ImageModel(string id,string location , string imageType)
    {
      Id = id;
      Location = location;
      ImageType = imageType;
    }
    public ImageModel(IFormFile image)
    {
      string id = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      Id = id;
      if (image.Length > 0)
      {
        using (var ms = new MemoryStream())
        {
          image.CopyTo(ms);
          var filebytes = ms.ToArray();
          Image = Convert.ToBase64String(filebytes);
        }
      }
      Location = null;
    }
    [Key]
    public string Id { get; set; }
    public string Image { get; set; }
    public string Location { get; set; }
    public string ImageType { get; set; }
  }
}
