using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class NavigationPanelVM : ViewModelBase
    { 
        private IInfSerctionService _infSerctionService;
        private IMessageService _messageService;
        private IPageService _pageService;
        private IProjectService _projectService;
        private IReportService _reportService;
        private ITaskService _taskService;
        private ITrackService _trackService;
        private ILoadFileService _loadFileService;

        private InfSectionDTO _currentInfSectionDTO;
        private PageDTO _currentPageDTO;

        private object _currentViewPanel;
        public object CurrentViewPanel
        {
            get { return _currentViewPanel; }
            set { _currentViewPanel = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProjectDTO> _projects;
        public ObservableCollection<ProjectDTO> Projects
        {
            get { return _projects; }
            set { _projects = value; OnPropertyChanged(); }
        }

        private ProjectDTO _currentProject;
        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, _currentProject); OnPropertyChanged(); }
        }

        public ICommand InfSectionCommand { get; set; }
        public ICommand TaskCommand { get; set; }
        public ICommand TrackCommand {  get; set; }
        public ICommand MessagesCommand {  get; set; }

        public ICommand PageCommand { get; set; }
        public ICommand PageCurrentCommand { get; set; }

        private void InfSection(object obj) => CurrentViewPanel = new InfSectionVM(_infSerctionService, _pageService, CurrentProject);
        private void Task(object obj) => CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject);
        private void Track(object obj) => CurrentViewPanel = new TrackVM(_trackService, _taskService, _loadFileService);
        private void Message(object obj) => CurrentViewPanel = new MessageVM(_taskService, _messageService);

        private void Page(object obj)
        {
            _currentInfSectionDTO = _currentInfSectionDTO == null ? ((InfSectionVM)CurrentViewPanel).SelectedSection : _currentInfSectionDTO;
            CurrentViewPanel = new PagesVM(_pageService, _currentInfSectionDTO);
        }

        private void PageCurrent(object obj)
        {
            _currentPageDTO = _currentPageDTO == null ? ((PagesVM)CurrentViewPanel).SelectedPage : _currentPageDTO;
            CurrentViewPanel = new PageCurrentVM(_pageService, _currentPageDTO);
        }

        public NavigationPanelVM(IInfSerctionService infSectionService, IMessageService messageService, IPageService pageService, IProjectService projectService,
                                 IReportService reportService, ITaskService taskService, ITrackService trackService, ILoadFileService loadFileService)
        {
            _infSerctionService = infSectionService;
            _messageService = messageService;
            _pageService = pageService;
            _projectService = projectService;
            _reportService = reportService;
            _taskService = taskService;
            _trackService = trackService;
            _loadFileService = loadFileService;

            InfSectionCommand = new RelayCommand(InfSection);
            TaskCommand = new RelayCommand(Task);
            TrackCommand = new RelayCommand(Track);
            MessagesCommand = new RelayCommand(Message);

            PageCommand = new RelayCommand(Page);
            PageCurrentCommand = new RelayCommand(PageCurrent);

            CurrentProject = _projectService.GetProjects().FirstOrDefault();

            CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject);
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
        }
    }
}
