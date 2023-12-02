using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DTO;
using BLL.Interfaces;
using System.Windows.Input;
using BLL.Services;

namespace ProjectSystems.ViewModel
{
    public class MainWindowVM : ViewModelBase
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

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand WorkerCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand ProjectsCommand { get; set; }
        public ICommand NavigationPanelCommand { get; set; }


        private void NavigationPanel(object obj) => CurrentView = new AdministrationProjectAndInfSections.NavigationPanelVM(_infSerctionService, _messageService,
                                                                                                                            _pageService,_projectService, _reportService,
                                                                                                                            _taskService, _trackService);
        private void Worker(object obj) => CurrentView = new WorkersVM(_workerService, _positionService);
        private void Settings(object obj) => CurrentView = new SettingsVM();
        private void Projects(object obj) => CurrentView = new ProjectsVM(_projectService);

        public MainWindowVM(IInfSerctionService infSerctionService, IMessageService messageService,
                              IPageService pageService, IPositionService positionService,
                              IProjectService projectService, IReportService reportService,
                              ITaskService taskService, ITrackService trackService,
                              IWorkerService workerService)
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

            NavigationPanelCommand = new RelayCommand(NavigationPanel);
            WorkerCommand = new RelayCommand(Worker);
            SettingsCommand = new RelayCommand(Settings);
            ProjectsCommand = new RelayCommand(Projects);

            CurrentView = new AdministrationProjectAndInfSections.NavigationPanelVM(_infSerctionService, _messageService,
                                                                                    _pageService, _projectService, _reportService,
                                                                                    _taskService, _trackService);
        }
    }
}
