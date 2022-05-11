using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericController<User>
    {
        private string ImageDirectory = Environment.GetEnvironmentVariable("LocalAppData") + "\\TravelGuide" + "\\Users";

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register([FromBody]UserWrapper user)
        {
            try
            {
                var checkUser = await repository.SelectAsync(u => u.Username == user.User.Username || u.Email == user.User.Email);

                if (checkUser != null)
                {
                    return Ok(false);
                }

                using (var ms = new MemoryStream(user.Image))
                {
                    if (!Directory.Exists(ImageDirectory))
                    {
                        Directory.CreateDirectory(ImageDirectory);
                    }

                    var image = Image.FromStream(ms);
                    user.User.ImagePath = ImageDirectory + "//" + user.User.Username + ".png";
                    image.Save(user.User.ImagePath);

                    await repository.Insert(user.User);

                    return Ok(true);
                }
            }
            catch(Exception)
            {
                return Ok(false);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login([FromBody]User user)
        {
            try
            {
                var checkUser = await repository.SelectAsync(u => u.Username == user.Username && u.Password == user.Password);

                if (checkUser != null)
                {
                    return Ok(true);
                }

                return Ok(false);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<UserWrapper>> GetAll()
        {
            List<UserWrapper> userWrappers = new List<UserWrapper>();
            var users = await repository.SelectAllAsync();

            foreach (var user in users)
            {
                var imageBytes = await System.IO.File.ReadAllBytesAsync(user.ImagePath);

                userWrappers.Add(new UserWrapper(user, imageBytes));
            }

            return Ok(userWrappers);
        }
    }
}
