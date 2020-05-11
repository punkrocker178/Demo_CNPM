using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private ImageService imageService;
        private ItemService itemService;

        public ImageController(StoreContext context)
        {
            imageService = new ImageService(context);
            itemService = new ItemService(context);
        }

        // GET: api/<controller>
        [HttpGet("GetImage/{id}")]
        public IActionResult GetImage(int id)
        {
            try
            {
                Image image = imageService.Get(id);
                var imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/ViewImage/{image.Title}";
                return Ok(new { id = image.Id, title = image.Title, url = imageUrl });
            } catch (Exception e)
            {
                return NotFound("Failed to get");
            }
        }

        [HttpGet("ViewImage/{fileName}")]
        public ActionResult<Image> ViewImage(string fileName)
        {
            try
            {
                Image imageEntity = imageService.GetByName(fileName);
                var image = System.IO.File.OpenRead(imageEntity.Path);
                return File(image, "image/jpeg");
            }
            catch (Exception e)
            {
                return NotFound("Failed to get");
            }
        }


        // POST api/<controller>
        [HttpPost("Upload/Item/{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostAsync(int id, [FromForm] IFormFile file)
        {
            try
            {
                long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                string fileName = unixTimestamp + "_" + file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/Image/ViewImage/{fileName}";
                bool result = imageService.Add(new Image(fileName, path), itemService.GetItem(id));
                if (result)
                {
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(bits);
                    }
                    return Ok(new { url });
                }
                return UnprocessableEntity();
            } catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
           
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
