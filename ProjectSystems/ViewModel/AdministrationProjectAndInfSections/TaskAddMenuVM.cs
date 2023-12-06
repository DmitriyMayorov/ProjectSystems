using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TaskAddMenuVM : ViewModelBase
    {
        ITaskService _taskService;
        IProjectService _projectService;
        IWorkerService _workerService;

        Notifier _notifier;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProjectDTO> _projects;
        public ObservableCollection<ProjectDTO> Projects
        {
            get { return _projects; }
            set { _projects = value; OnPropertyChanged(); }
        }

        private ProjectDTO _selectdes_project;
        public ProjectDTO SelectedProject
        {
            get { return _selectdes_project; }
            set { _selectdes_project = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _states;
        public ObservableCollection<string> Status
        {
            get { return _states; }
            set { _states = value; OnPropertyChanged(); }
        }

        private string _selectdes_status;
        public string SelectedStatus
        {
            get { return _selectdes_status; } 
            set { _selectdes_status = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _category;
        public ObservableCollection<string> Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }

        private string _selectdes_category;
        public string SelectedCategory
        {
            get { return _selectdes_category; }
            set { _selectdes_category = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _priorities;
        public ObservableCollection<string> Priorities
        {
            get { return _priorities; }
            set { _priorities = value; OnPropertyChanged(); }
        }

        private string _selectdes_priorities;
        public string SelectedPriority
        {
            get { return _selectdes_priorities; }
            set { _selectdes_priorities = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WorkerDTO> _analyst_workers;
        public ObservableCollection<WorkerDTO> AnalystWorkers
        {
            get { return _analyst_workers; }
            set { _analyst_workers = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selected_analyst;
        public WorkerDTO SelectedAnalyst
        {
            get { return _selected_analyst; }
            set { _selected_analyst = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WorkerDTO> _coder_workers;
        public ObservableCollection<WorkerDTO> CoderWorkers
        {
            get { return _coder_workers; }
            set { _coder_workers = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selected_coder;
        public WorkerDTO SelectedCoder
        {
            get { return _selected_coder; }
            set { _selected_coder = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selected_techlid;
        public WorkerDTO SelectedTechlid
        {
            get { return _selected_techlid; }
            set { _selected_techlid = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WorkerDTO> _tester_workers;
        public ObservableCollection<WorkerDTO> TesterWorkers
        {
            get { return _tester_workers; }
            set { _tester_workers = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selected_tester;
        public WorkerDTO SelectedTester
        {
            get { return _selected_tester; }
            set { _selected_tester = value; OnPropertyChanged(); }
        }

        private DateTime _selected_date;
        public DateTime SelectedDate
        {
            get { return _selected_date; }
            set { _selected_date = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand {  get; set; } 

        private void AddTask(object obj)
        {
            if (Name == null || Description == null || SelectedProject == null)
            {
                _notifier.ShowError("Выберите все нужные данные");
                return;
            }
            TaskDTO temp = new TaskDTO();
            temp.Name = Name;
            temp.Description = Description;
            temp.Project = SelectedProject;
            temp.Category = SelectedCategory;
            temp.State = SelectedStatus;
            temp.Priority = SelectedPriority;
            temp.WorkerAnalyst = SelectedAnalyst;
            temp.WorkerCoder = SelectedCoder;
            temp.WorkerTechlid = SelectedTechlid;
            temp.WorkerTester = SelectedTester;
            temp.IDProject = SelectedProject.Id;
            temp.IDWorkerAnalyst = SelectedAnalyst.Id;
            temp.IDWorkerCoder = SelectedCoder.Id;
            temp.IDWorkerMentor = SelectedTechlid.Id;
            temp.IDWorkerTester = SelectedTester.Id;
            
            _taskService.CreateTask(temp);
            _notifier.ShowSuccess("Успешное создание задания");
        }

        public TaskAddMenuVM(ITaskService taskService, IProjectService projectService, IWorkerService workerService) 
        {
            _taskService = taskService;
            _projectService = projectService;
            _workerService = workerService;

            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
            Status = new ObservableCollection<string>(new List<string>() {"Plan", "InProgress", "CodeRewiew", "Stage", "Test", "Ready"});
            Category = new ObservableCollection<string>(new List<string>() { "NEW", "BFX" });
            Priorities = new ObservableCollection<string>(new List<string> { "low", "medium", "high" });
            AnalystWorkers = new ObservableCollection<WorkerDTO>(_workerService.GetWorkers());
            CoderWorkers = new ObservableCollection<WorkerDTO>(_workerService.GetWorkers());
            TesterWorkers = new ObservableCollection<WorkerDTO>(_workerService.GetWorkers());

            SelectedDate = DateTime.Now;

            AddCommand = new RelayCommand(AddTask);

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
        }
    }
}
