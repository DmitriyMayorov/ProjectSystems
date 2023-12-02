namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Track")]
    public partial class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IDTask { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTrack { get; set; }

        public int CountHours { get; set; }

        public int IDWorker { get; set; }

        public virtual Task Task { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
