using System;
using System.Collections.Generic;
using System.Text;

namespace TravelGuide.Services
{
    public class User
    {
        #region Properties

        public int? Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        #endregion
    }
}
