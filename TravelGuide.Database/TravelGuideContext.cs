using Microsoft.EntityFrameworkCore;
using TravelGuide.Database.Entities;

namespace TravelGuide.Database
{
    public class TravelGuideContext : DbContext
    {
        #region DbSets

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Landmark> Landmarks { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Constructor

        public TravelGuideContext()
        {
        }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.0.102;Database=TravelGuide;User Id=sa;Password=123456;");
        }

        #endregion
    }
}
