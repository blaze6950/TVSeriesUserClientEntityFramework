namespace TVSeriesUserClientEntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TVSeriesModel : DbContext
    {
        public TVSeriesModel() : base("name=TVSeriesModelCS")
        {
        }

        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<TVSeriesTable> TVSeriesTables { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>()
                .HasMany(e => e.TVSeriesTables)
                .WithRequired(e => e.Channel)
                .HasForeignKey(e => e.Channel_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.TVSeriesTables)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("TVSeriesGenres").MapRightKey("TVSeries_Id"));

            modelBuilder.Entity<TVSeriesTable>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.TVSeriesTable)
                .HasForeignKey(e => e.Id_TVSerial);

            modelBuilder.Entity<TVSeriesTable>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.TVSeriesTable)
                .HasForeignKey(e => e.Id_TVSerial);

            modelBuilder.Entity<TVSeriesTable>()
                .HasMany(e => e.Users)
                .WithMany(e => e.TVSeriesTables)
                .Map(m => m.ToTable("UsersTVSeriesTable").MapLeftKey("Id_TVSerial").MapRightKey("Id_User"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_User);
        }
    }
}
