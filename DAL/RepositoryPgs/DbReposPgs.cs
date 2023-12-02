using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryPgs
{
    public class DbReposPgs : IDbRepos
    {
        private ProjectSystemContext db;
        private InfSectionRepositoryPgs InfSectionRepositoryPgs;
        private MessageRepositoryPgs MessageRepositoryPgs;
        private PageRepositoryPgs PageRepositoryPgs;
        private PositionRepositoryPgs PositionRepositoryPgs;
        private ProjectRepositoryPgs ProjectRepositoryPgs;
        private ReportRepositoryPgs ReportRepositoryPgs;
        private TaskRepositoryPgs TaskRepositoryPgs;
        private TrackRepositoryPgs TrackRepositoryPgs;
        private WorkerRepositoryPgs WorkerRepositoryPgs;

        public DbReposPgs()
        {
            db = new ProjectSystemContext();
        }

        public IRepository<Project> Projects
        { 
            get
            {
                if (ProjectRepositoryPgs == null)
                    ProjectRepositoryPgs = new ProjectRepositoryPgs(db);
                return ProjectRepositoryPgs;
            }
        }

        public IRepository<DAL.EF.Task> Tasks
        {
            get
            {
                if (TaskRepositoryPgs == null)
                    TaskRepositoryPgs = new TaskRepositoryPgs(db);
                return TaskRepositoryPgs;
            }
        }

        public IRepository<Worker> Workers
        {
            get
            {
                if (WorkerRepositoryPgs == null)
                    WorkerRepositoryPgs = new WorkerRepositoryPgs(db);
                return WorkerRepositoryPgs;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (PositionRepositoryPgs == null)
                    PositionRepositoryPgs = new PositionRepositoryPgs(db);
                return PositionRepositoryPgs;
            }
        }

        public IRepository<Track> Tracks
        {
            get
            {
                if (TrackRepositoryPgs == null)
                    TrackRepositoryPgs = new TrackRepositoryPgs(db);
                return TrackRepositoryPgs;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (MessageRepositoryPgs == null)
                    MessageRepositoryPgs = new MessageRepositoryPgs(db);
                return MessageRepositoryPgs;
            }
        }

        public IRepository<InfSection> InfSections
        {
            get
            {
                if (InfSectionRepositoryPgs == null)
                    InfSectionRepositoryPgs = new InfSectionRepositoryPgs(db);
                return InfSectionRepositoryPgs;
            }
        }

        public IRepository<Page> Pages
        {
            get
            {
                if (PageRepositoryPgs == null)
                    PageRepositoryPgs = new PageRepositoryPgs(db);
                return PageRepositoryPgs;
            }
        }

        public IReportRepository Reports
        {
            get
            {
                if (ReportRepositoryPgs == null)
                    ReportRepositoryPgs = new ReportRepositoryPgs(db);
                return ReportRepositoryPgs;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
