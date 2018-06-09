using System.Linq;

namespace TVSeriesUserClientEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TVSeriesTable")]
    public partial class TVSeriesTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TVSeriesTable()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
            Genres = new HashSet<Genre>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Image { get; set; }

        public int? YearOfIssue { get; set; }

        public int? Seasons { get; set; }

        [StringLength(3000)]
        public string Desription { get; set; }

        public int Channel_Id { get; set; }

        [NotMapped]
        public double AverageRating
        {
            get
            {
                if (Ratings.Count > 0)
                {
                    return Ratings.AsQueryable().Average(r => r.Mark);
                }
                return 0;
            }
        }

        public virtual Channel Channel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
