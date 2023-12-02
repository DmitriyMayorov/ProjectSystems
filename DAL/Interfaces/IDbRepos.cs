using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepos
    {
        IRepository<Project> Projects { get; }
        IRepository<DAL.EF.Task> Tasks { get; }
        IRepository<Worker> Workers { get; }
        IRepository<Position> Positions { get; }
        IRepository<Track> Tracks { get; }
        IRepository<Message> Messages { get; }
        IRepository<InfSection> InfSections { get; }
        IRepository<Page> Pages { get; }
        IReportRepository Reports { get; }
        int Save();
    }
}
