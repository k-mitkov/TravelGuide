using System.ComponentModel.DataAnnotations.Schema;
using TravelGuide.Database.Interfaces;

namespace TravelGuide.Database.Entities
{
    public class Landmark : IEntity
    {
        #region Properties

        [Column("Id")]
        public int? Id { get; set; }

        [Column("Name1")]
        public string Name1 { get; set; }

        [Column("Name2")]
        public string Name2 { get; set; }

        [Column("Latitude")]
        public decimal Latitude { get; set; }

        [Column("Longitude")]
        public decimal Longitude { get; set; }

        [Column("Description1")]
        public string Description1 { get; set; }

        [Column("Description2")]
        public string Description2 { get; set; }

        [Column("ImagePath")]
        public string ImagePath { get; set; }

        #endregion
    }
}
