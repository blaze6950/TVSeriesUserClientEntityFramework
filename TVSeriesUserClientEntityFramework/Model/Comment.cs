namespace TVSeriesUserClientEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int Id { get; set; }

        public int Id_TVSerial { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextComment { get; set; }

        public virtual TVSeriesTable TVSeriesTable { get; set; }
    }
}
