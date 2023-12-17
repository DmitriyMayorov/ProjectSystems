using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Serilog;

namespace ProjectSystems.ViewModel
{
    public class ApplicationVM : ViewModelBase
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

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand toFirstWindow { get; set; }
        public ICommand toSecondWindowAsCoder { get; set; }
        public ICommand toSecondWindowAsAnalyst { get; set; }
        public ICommand toSecondWindowAsTester { get; set; }

        private void toFirstWindowExecute(object obj)
        {
            CurrentView = new AuthorizationVM();
            Log.Information("Изменение отображаемого экрана на авторизацию");
        }

        private void toSecondWindowAsCoderExecute(object obj)
        {
            CurrentView = new MainWindowVM(_infSerctionService, _messageService,
                                           _pageService, _positionService,
                                           _projectService, _reportService,
                                           _taskService, _trackService,
                                           _workerService, _loadFileService, "Coder");
            Log.Information("Изменение отображаемого экрана на экран приложение в роли программиста");
        }

        private void toSecondWindowAsAnalystExecute(object obj)
        {
            CurrentView = new MainWindowVM(_infSerctionService, _messageService,
                                           _pageService, _positionService,
                                           _projectService, _reportService,
                                           _taskService, _trackService,
                                           _workerService, _loadFileService, "Analyst");
            Log.Information("Изменение отображаемого экрана на экран приложения в роли аналитика");
        }

        private void toSecondWindowAsTesterExecute(object obj)
        {
            CurrentView = new MainWindowVM(_infSerctionService, _messageService,
                                           _pageService, _positionService,
                                           _projectService, _reportService,
                                           _taskService, _trackService,
                                           _workerService, _loadFileService, "Tester");
            Log.Information("Изменение отображаемого экрана на экран приложения в роли тестировщика");
        }

        public ApplicationVM(IInfSerctionService infSerctionService, IMessageService messageService,
                              IPageService pageService, IPositionService positionService,
                              IProjectService projectService, IReportService reportService,
                              ITaskService taskService, ITrackService trackService,
                              IWorkerService workerService, ILoadFileService loadFileService)
        {
            _infSerctionService = infSerctionService;
            _messageService = messageService;
            _pageService = pageService;
            _positionService = positionService;
            _projectService = projectService;
            _reportService = reportService;
            _taskService = taskService;
            _trackService = trackService;
            _workerService = workerService;
            _loadFileService = loadFileService;

            toFirstWindow = new RelayCommand(toFirstWindowExecute);
            toSecondWindowAsCoder = new RelayCommand(toSecondWindowAsCoderExecute);
            toSecondWindowAsAnalyst = new RelayCommand(toSecondWindowAsAnalystExecute);
            toSecondWindowAsTester = new RelayCommand(toSecondWindowAsTesterExecute);

            _currentView = new AuthorizationVM();

            Log.Information("Загрузка главного окна приложения");
        }
    }
}
