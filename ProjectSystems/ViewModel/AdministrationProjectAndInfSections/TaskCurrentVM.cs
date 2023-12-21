﻿using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Serilog;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using LiveCharts;
using System.Windows.Input;
using ToastNotifications.Messages;
using System.Windows.Shell;
using System.Runtime.Remoting;
using ProjectSystems.View.AdministrationProjectAndInfSections;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TaskCurrentVM : ViewModelBase
    {
        ITaskService _taskService;
        ITrackService _trackService;
        IWorkerService _workerService;

        TaskDTO _taskDTO;

        Notifier _notifier;
        private TrackAddMenu addMenu;

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

        private ObservableCollection<WorkerDTO> _workerAnalystDTO;
        public ObservableCollection<WorkerDTO> WorkerAnalystDTO
        {
            get { return _workerAnalystDTO; }
            set { _workerAnalystDTO = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selectedAnalystDTO;
        public WorkerDTO SelectedAnalystDTO
        {
            get { return _selectedAnalystDTO; }
            set { _selectedAnalystDTO = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WorkerDTO> _workerCoderDTO;
        public ObservableCollection<WorkerDTO> WorkerCoderDTO
        {
            get { return _workerCoderDTO; }
            set { _workerCoderDTO = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selectedCoderDTO;
        public WorkerDTO SelectedCoderDTO
        {
            get { return _selectedCoderDTO; }
            set { _selectedCoderDTO = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selectedTechlidDTO;
        public WorkerDTO SelectedTechlidDTO
        {
            get { return _selectedTechlidDTO; }
            set { _selectedTechlidDTO = value; OnPropertyChanged(); }
        }

        private ObservableCollection<WorkerDTO> _workerTesterDTO;
        public ObservableCollection<WorkerDTO> WorkerTesterDTO
        {
            get { return _workerCoderDTO; }
            set { _workerCoderDTO = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selectedTesterDTO;
        public WorkerDTO SelectedTesterDTO
        {
            get { return _selectedTesterDTO; }
            set { _selectedTesterDTO = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _priority;
        public ObservableCollection<string> Priority
        {
            get { return _priority; }
            set { _priority = value; OnPropertyChanged(); }
        }

        private string _selectedPriority;
        public string SelectedPriority
        {
            get { return _selectedPriority; }
            set { _selectedPriority = value; OnPropertyChanged(); }
        }

        private ChartValues<StatisticTrackDTO> _result;
        public ChartValues<StatisticTrackDTO> ResultsTrack
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> LabelsTrack { get; set; }

        private ChartValues<int> _countHours;
        public ChartValues<int> CountHoursTrack
        {
            get { return _countHours; }
            set { _countHours = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ICommand ToInProgressCommand { get; set; }
        public ICommand ToReviewCommand { get; set; }
        public ICommand ToStageCommand { get; set; }
        public ICommand ToTestCommand { get; set; }
        public ICommand ToReadyCoomand { get; set; }

        private void UpdateCommandExecute(object obj)
        {
            _taskService.UpdateTask(_taskDTO);
        }

        private void AddCommandExecute(object obj)
        {
            addMenu = new TrackAddMenu(_taskDTO);
            addMenu.ShowDialog();
            CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            LabelsTrack = new ObservableCollection<string>(new List<string>() { _taskDTO.State });
        }

        private void ToInProgressExecute(object obj)
        {
            if (_taskService.ToInProgress(_taskDTO))
            {
                _notifier.ShowSuccess("Задание перешло в стадию InProgress");
                _taskDTO.State = "InProgress";
                CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            }
            else
                _notifier.ShowWarning("В стадию InProgress можно перейти только из всех стадий, кроме Ready");
        }

        private void ToReviewExecute(object obj) 
        {
            if (_taskService.ToReview(_taskDTO))
            {
                _notifier.ShowSuccess("Задание перешло в стадию Review");
                _taskDTO.State = "Review";
                CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            }
            else
                _notifier.ShowWarning("В стадию Review можно перейти только из стадии InProgress");
        }

        private void ToStageExecute(object obj)
        {
            if (_taskService.ToStage(_taskDTO))
            {
                _notifier.ShowSuccess("Задание перешло в стадию Stage");
                _taskDTO.State = "Stage";
                CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            }
            else
                _notifier.ShowWarning("В стадию Stage можно перейти только из стадии Review");
        }

        private void ToTestExecute(object obj)
        {
            if (_taskService.ToTest(_taskDTO))
            {
                _notifier.ShowSuccess("Задание перешло в стадию Test");
                _taskDTO.State = "Test";
                CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            }
            else
                _notifier.ShowWarning("В стадию Test можно перейти только из стадии Stage");

        }

        private void ToReadyExecute(object obj)
        {
            if (_taskService.ToReady(_taskDTO))
            {
                _notifier.ShowSuccess("Задание перешло в стадию Ready");
                _taskDTO.State = "Ready";
                CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State) });
            }
            else
                _notifier.ShowWarning("В стадию Ready можно перейти только из стадии Test");
        }

        public TaskCurrentVM(ITaskService taskService, ITrackService trackService, IWorkerService workerService, TaskDTO taskDTO)
        {
            _taskService = taskService;
            _trackService = trackService;
            _workerService = workerService;

            _taskDTO = taskDTO;

            Name = _taskDTO.Name;
             Description = _taskDTO.Description;
            WorkerAnalystDTO = new ObservableCollection<WorkerDTO>(_workerService.GetAnalysts());
            SelectedAnalystDTO = _taskDTO.WorkerAnalyst;
            WorkerCoderDTO = new ObservableCollection<WorkerDTO>(_workerService.GetCoders());
            SelectedCoderDTO = _taskDTO.WorkerCoder;
            SelectedTechlidDTO = _taskDTO.WorkerCreater;
            WorkerTesterDTO = new ObservableCollection<WorkerDTO>(_workerService.GetTesters());
            SelectedTesterDTO = _taskDTO.WorkerTester;

            Priority = new ObservableCollection<string>(new List<string>() { "low", "medium", "high" });
            SelectedPriority = _taskDTO.Priority;

            CountHoursTrack = new ChartValues<int>(new List<int> { _trackService.GetSumHours(_taskDTO.Id, _taskDTO.State)});
            LabelsTrack = new ObservableCollection<string>(new List<string>() { "State" });

            UpdateCommand = new RelayCommand(UpdateCommandExecute);
            AddCommand = new RelayCommand(AddCommandExecute);

            ToInProgressCommand = new RelayCommand(ToInProgressExecute);
            ToReviewCommand = new RelayCommand(ToReviewExecute);
            ToStageCommand = new RelayCommand(ToStageExecute);
            ToTestCommand = new RelayCommand(ToTestExecute);
            ToReadyCoomand = new RelayCommand(ToReadyExecute);

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