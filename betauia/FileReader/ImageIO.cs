using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace betauia.FileReader
{
  public class ImageIO : IimageIO
  {
    public async Task<FileStream> GetFile(string location)
    {
      return System.IO.File.OpenRead(location);
    }

    public async Task WriteFile(string filepath,IFormFile file)
    {
      using (var fileStream = new FileStream(filepath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }
    }
  }
}
