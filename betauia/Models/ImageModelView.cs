using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace betauia.Models
{
  public class ImageModelView
  {
    public string Name { get; set; }
    public IFormFile Image { get; set; }
  }
}
