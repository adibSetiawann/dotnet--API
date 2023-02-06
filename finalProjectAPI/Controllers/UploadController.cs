using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi
{
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage(IFormFileCollection file)
        {
            bool result = false;
            try
            {
                var files = Request.Form.Files;
                foreach (IFormFile source in file)
                {
                    string filename = source.FileName;
                    string filePath = GetFilePath(filename);

                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }

                    string imgPath = filePath + $"/{filename}";

                    if (!System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    using (FileStream stream = System.IO.File.Create(imgPath))
                    {
                        await source.CopyToAsync(stream);
                        result = true;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return Ok(result);
        }

        [NonAction]
        private string GetFilePath(string productCode)
        {
            string[] paths =
            {
                @"ImageData",
                productCode
            };
            string fullPath = Path.Combine(paths);
            return fullPath;
        }
    }
}
