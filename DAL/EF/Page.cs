namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Page")]
    public partial class Page
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string TextSection { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSection { get; set; }

        public virtual InfSection InfSection { get; set; }
    }
}
