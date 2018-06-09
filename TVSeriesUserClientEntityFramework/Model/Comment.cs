namespace TVSeriesUserClientEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_User { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_TVSerial { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextComment { get; set; }

        public virtual TVSeriesTable TVSeriesTable { get; set; }

        public virtual User User { get; set; }
    }
}
