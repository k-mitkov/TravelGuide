using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TravelGuide.Database;
using TravelGuide.Database.Entities;
using TravelGuide.Database.Interfaces;
using TravelGuide.Database.Repositories;

namespace TravelGuide.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandmarkController : GenericController<Landmark>
    {
        public override async Task<ActionResult> Post([FromBody] Landmark entity)
        {
            if (entity != null)
            {
                HttpClient client = new HttpClient();

                var _mediaTypeJson = new MediaTypeWithQualityHeaderValue("application/json");

                client.BaseAddress = new Uri("http://localhost:5002/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(_mediaTypeJson);

                var response = await client.PostAsJsonAsync("", entity);

                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetImageById/{id}")]
        public async Task<ActionResult<byte[]>> GetImageById([FromRoute] int id)
        {
            var landmark = await repository.SelectAsync(u => u.Id == id);

            var bytes = await System.IO.File.ReadAllBytesAsync(landmark.ImagePath);

            return Ok(bytes);
        }
    }
}
