using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace betauia.FileReader
{
  public interface IimageIO
  {
    Task<FileStream> GetFile(string location);
    Task WriteFile(string filepath,IFormFile file);
  }
}
