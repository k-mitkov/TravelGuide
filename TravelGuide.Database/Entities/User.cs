using System.ComponentModel.DataAnnotations.Schema;
using TravelGuide.Database.Interfaces;

namespace TravelGuide.Database.Entities
{
    public class  User : IEntity
    {
        #region Properties

        [Column("Id")]
        public int? Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("ImagePath")]
        public string ImagePath { get; set; }

        #endregion
    }
}
