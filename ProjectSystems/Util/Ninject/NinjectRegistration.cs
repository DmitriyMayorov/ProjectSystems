using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSystems.Util.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IWorkerService>().To<WorkerService>();
            Bind<IPositionService>().To<PositionService>();
            Bind<ITrackService>().To<TrackService>();
            Bind<IMessageService>().To<MessageService>();
            Bind<IInfSerctionService>().To<InfSectionService>();
            Bind<IPageService>().To<PageService>();
            Bind<IReportService>().To<ReportService>();
        }
    }
}
