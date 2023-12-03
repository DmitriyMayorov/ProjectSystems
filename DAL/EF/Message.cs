namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Message")]
    public partial class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TextMessage { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateMessage { get; set; }

        public int IDTask { get; set; }

        public int IDWorker { get; set; }

        public virtual Task Task { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
