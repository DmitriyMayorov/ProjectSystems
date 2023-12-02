namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Messages = new HashSet<Message>();
            Tracks = new HashSet<Track>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? IDWorkerCoder { get; set; }

        public int? IDWorkerAnalyst { get; set; }

        public int? IDWorkerMentor { get; set; }

        public int? IDWorkerTester { get; set; }

        public int IDProject { get; set; }

        [Required]
        [StringLength(128)]
        public string Category { get; set; }

        [Required]
        [StringLength(128)]
        public string State { get; set; }

        [Required]
        [StringLength(16)]
        public string Priority { get; set; }

        public int? IDWorkerCreater { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Deadline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        public virtual Project Project { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Tracks { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual Worker Worker1 { get; set; }

        public virtual Worker Worker2 { get; set; }

        public virtual Worker Worker3 { get; set; }

        public virtual Worker Worker4 { get; set; }
    }
}
