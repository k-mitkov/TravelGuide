using System.ComponentModel.DataAnnotations.Schema;
using TravelGuide.Database.Interfaces;

namespace TravelGuide.Database.Entities
{
    public class Comment : IEntity
    {
        #region Properties 

        [Column("Id")]
        public int? Id { get; set; }

        [Column("Content")]
        public string Content { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("LandmarkId")]
        public int LandmarkId { get; set; }

        #endregion
    }
}
