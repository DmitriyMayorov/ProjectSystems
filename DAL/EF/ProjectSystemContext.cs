using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EF
{
    public partial class ProjectSystemContext : DbContext
    {
        public ProjectSystemContext()
            : base("name=ProjectSystemContext")
        {
        }

        public virtual DbSet<InfSection> InfSections { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfSection>()
                .HasOptional(e => e.Page)
                .WithRequired(e => e.InfSection);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Workers)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.IDPosition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.InfSections)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.IDProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.IDProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.IDTask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Tracks)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.IDTask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Worker)
                .HasForeignKey(e => e.IDWorker)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Worker)
                .HasForeignKey(e => e.IDWorkerAnalyst);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tasks1)
                .WithOptional(e => e.Worker1)
                .HasForeignKey(e => e.IDWorkerCoder);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tasks2)
                .WithOptional(e => e.Worker2)
                .HasForeignKey(e => e.IDWorkerCreater);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tasks3)
                .WithOptional(e => e.Worker3)
                .HasForeignKey(e => e.IDWorkerMentor);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tasks4)
                .WithOptional(e => e.Worker4)
                .HasForeignKey(e => e.IDWorkerTester);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Tracks)
                .WithRequired(e => e.Worker)
                .HasForeignKey(e => e.IDWorker)
                .WillCascadeOnDelete(false);
        }
    }
}
