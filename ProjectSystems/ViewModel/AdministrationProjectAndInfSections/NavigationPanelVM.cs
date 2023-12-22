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
using Serilog;
using System.Diagnostics;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using System.Windows;
using ToastNotifications.Messages;

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
        private IWorkerService _workerService;

        private Notifier _notifier;

        private InfSectionDTO _currentInfSectionDTO;
        private PageDTO _currentPageDTO;
        private TaskDTO _currentTaskDTO;

        private string _status;

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
            set { _currentProject = value; CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, _currentProject, _status); OnPropertyChanged(); }
        }

        public ICommand InfSectionCommand { get; set; }
        public ICommand TrackCommand {  get; set; }
        public ICommand MessagesCommand {  get; set; }

        public ICommand PageCommand { get; set; }
        public ICommand PageCurrentCommand { get; set; }

        public ICommand TaskCommand { get; set; }
        public ICommand TaskCurrentCommand { get; set; }

        private void InfSection(object obj)
        {
            try
            {
                CurrentViewPanel = new InfSectionVM(_infSerctionService, _pageService, CurrentProject);
                Log.Information("Выбор в навигационной панели пользовательского элемента с информационными секциями");
            }
            catch (Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно информационных секций. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно информационных секций - " + ex.Message);
            }
        }

        private void Task(object obj)
        {
            try
            {
                CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject, _status);
                Log.Information("Выбор в навигационной панели пользовательского элемента с заданиями");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно заданий. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно заданий - " + ex.Message);
            }
        }

        private void Track(object obj)
        {
            try
            {
                CurrentViewPanel = new TrackVM(_trackService, _taskService, _loadFileService, CurrentProject, _status);
                Log.Information("Выбор в навигационной панели пользовательского элемента с треккингом времени для заданий");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно треккинга. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно треккинга - " + ex.Message);
            }
        }

        private void Message(object obj)
        {
            try
            {
                CurrentViewPanel = new MessageVM(_taskService, _messageService, CurrentProject, _status);
                Log.Information("Выбор в навигационной панели пользовательского элемента с сообщениями по заданию");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно сообщений. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно сообщений - " + ex.Message);
            }
        }

        private void Page(object obj)
        {
            try
            {
                if (CurrentViewPanel is InfSectionVM)
                    _currentInfSectionDTO = ((InfSectionVM)CurrentViewPanel).SelectedSection;

                Log.Debug("Выбранная информационная секция - Название: " + _currentInfSectionDTO.Name);
                CurrentViewPanel = new PagesVM(_pageService, _currentInfSectionDTO);
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно страниц информационных секций. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно страниц информационных секций - " + ex.Message);
            }
        }

        private void PageCurrent(object obj)
        {
            try
            {
                if (CurrentViewPanel is PagesVM)
                    _currentPageDTO = ((PagesVM)CurrentViewPanel).SelectedPage;

                Log.Debug("Выбранная страница - Название: " + _currentPageDTO.Name);
                CurrentViewPanel = new PageCurrentVM(_pageService, _currentPageDTO);
            }
            catch( Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно инфомарционной страницы. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно информационной страницы - " + ex.Message);
            }
        }

        private void TaskCurrent(object obj)
        {
            try
            {
                _currentTaskDTO = ((TasksVM)CurrentViewPanel).SelectedTask;
                Log.Debug("Выбранное задание - Название: " + _currentTaskDTO.Name);
                CurrentViewPanel = new TaskCurrentVM(_taskService, _trackService, _workerService, _currentTaskDTO, _status);
            }
            catch (Exception ex)
            {
                _notifier.ShowError("Ошибка при переключении на окно задания. Смотрите журнал логирования");
                Log.Error("Ошибка при переключении на окно задания - " + ex.Message);
            }
        }

        public NavigationPanelVM(IInfSerctionService infSectionService, IMessageService messageService, IPageService pageService, IProjectService projectService,
                                 IReportService reportService, ITaskService taskService, ITrackService trackService, ILoadFileService loadFileService, IWorkerService workerService, string status)
        {
            _infSerctionService = infSectionService;
            _messageService = messageService;
            _pageService = pageService;
            _projectService = projectService;
            _reportService = reportService;
            _taskService = taskService;
            _trackService = trackService;
            _loadFileService = loadFileService;
            _workerService = workerService;
            _status = status;

            InfSectionCommand = new RelayCommand(InfSection);
            TrackCommand = new RelayCommand(Track);
            MessagesCommand = new RelayCommand(Message);
            PageCommand = new RelayCommand(Page);
            PageCurrentCommand = new RelayCommand(PageCurrent);
            TaskCommand = new RelayCommand(Task);
            TaskCurrentCommand = new RelayCommand(TaskCurrent);

            CurrentProject = _projectService.GetProjects().FirstOrDefault();

            CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject, _status);
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());

            _notifier = new Notifier(cfg =>
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
            });

            Log.Information("Инициализация конструктора навигационной модели проекта");
        }
    }
}
