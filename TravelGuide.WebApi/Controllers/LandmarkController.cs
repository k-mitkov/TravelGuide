using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.WebApi.Helpers;

namespace TravelGuide.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandmarkController : GenericController<Landmark>
    {
        private string ImageDirectory = Environment.GetEnvironmentVariable("LocalAppData") + "\\TravelGuide" + "\\Landmarks";

        [HttpPost("Post")]
        public async Task<ActionResult> Post([FromBody] LandmarkWrapper landmark)
        {
            try
            {
                if (landmark != null)
                {
                    using (var ms = new MemoryStream(landmark.Image))
                    {
                        if (!Directory.Exists(ImageDirectory))
                        {
                            Directory.CreateDirectory(ImageDirectory);
                        }

                        var image = Image.FromStream(ms);
                        landmark.Landmark.ImagePath = (ImageDirectory + "//" + (landmark.Landmark.Name1 ?? landmark.Landmark.Name2) + ".png").GetPath();

                        image.Save(landmark.Landmark.ImagePath);
                    }

                    HttpClient client = new HttpClient();

                    var _mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

                    client.BaseAddress = new Uri("http://localhost:5002/");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(_mediaTypeJson);

                    var response = await client.PostAsJsonAsync("", landmark);

                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LandmarkWrapper>>> GetAll()
        {
            try
            {
                List<LandmarkWrapper> landmarkWrappers = new List<LandmarkWrapper>();
                var landmarks = await repository.SelectAllAsync();

                foreach (var landmark in landmarks)
                {
                    var imageBytes = await System.IO.File.ReadAllBytesAsync(landmark.ImagePath);

                    landmarkWrappers.Add(new LandmarkWrapper(landmark, imageBytes));
                }

                return Ok(landmarkWrappers);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("GetLandmarkByCondition/{query}")]
        public async Task<ActionResult<LandmarkWrapper>> GetLandmarkByCondition([FromRoute] string query)
        {
            try
            {
                var p = Expression.Parameter(typeof(Landmark), "i");

                var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, query);

                var typedExpression = (Expression<Func<Landmark, bool>>)e;

                var dbLandmark = await repository.SelectAsync(typedExpression);
                var imageBytes = await System.IO.File.ReadAllBytesAsync(dbLandmark.ImagePath);

                LandmarkWrapper landmark = new LandmarkWrapper()
                {
                    Landmark = dbLandmark,
                    Image = imageBytes
                };

                return Ok(landmark);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
