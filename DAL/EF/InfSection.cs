namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.InfSection")]
    public partial class InfSection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int IDProject { get; set; }

        public virtual Project Project { get; set; }

        public virtual Page Page { get; set; }
    }
}
