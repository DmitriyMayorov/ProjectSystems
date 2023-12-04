using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Interfaces;
using Ninject;
using ProjectSystems.ViewModel;
using ProjectSystems.Util.Ninject;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace ProjectSystems.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IInfSerctionService _infSerctionService;
        IMessageService _messageService;
        IPageService _pageService;
        IPositionService _positionService;
        IProjectService _projectService;
        IReportService _reportService;
        ITaskService _taskService;
        ITrackService _trackService;
        IWorkerService _workerService;
        ILoadFileService _loadFileService;

        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            _infSerctionService = kernel.Get<IInfSerctionService>();
            _messageService = kernel.Get<IMessageService>();
            _pageService = kernel.Get<IPageService>();
            _positionService = kernel.Get<IPositionService>();
            _projectService = kernel.Get<IProjectService>();
            _reportService = kernel.Get<IReportService>();
            _taskService = kernel.Get<ITaskService>();
            _trackService = kernel.Get<ITrackService>();
            _workerService = kernel.Get<IWorkerService>();
            _loadFileService = kernel.Get<ILoadFileService>();

            DataContext = new MainWindowVM(_infSerctionService, _messageService, _pageService, _positionService, _projectService, _reportService, _taskService, _trackService, _workerService, _loadFileService);
        }

/*        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });*/
    }
}
