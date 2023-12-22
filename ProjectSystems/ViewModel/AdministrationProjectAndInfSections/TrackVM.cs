using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using System.Collections.ObjectModel;
using BLL.Interfaces;
using BLL.DTO;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Shell;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using System.Windows;
using ToastNotifications.Messages;
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TrackVM : ViewModelBase
    {
        ITrackService _trackService;
        ITaskService _taskService;
        ILoadFileService _loadFileService;

        Notifier _notifier;

        public double YAxis { get; set; }
        private TrackAddMenu addMenu;

        private ObservableCollection<TaskDTO> _tasks;
        public ObservableCollection<TaskDTO> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; OnPropertyChanged(); }
        }

        private TaskDTO _selected_task;
        public TaskDTO SelectedTask
        {
            get { return _selected_task; }
            set { _selected_task = value; OnPropertyChanged(); }
        }

        public ICommand ChoiceTask {  get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand LoadPdfFileCommand { get; set; }

        public void ChoiceTaskExecute(object obj)
        {
            try
            {
                if (SelectedTask == null)
                {
                    _notifier.ShowWarning("Выберите задание");
                    return;
                }
                List<StatisticTrackDTO> Stat = new List<StatisticTrackDTO>();
                foreach (var status in LabelsTrack)
                {
                    StatisticTrackDTO temp = new StatisticTrackDTO();
                    temp.NameStatus = status;
                    temp.CountHours = _trackService.GetSumHours(SelectedTask.Id, status);

                    Stat.Add(temp);
                }
                ResultsTrack = Stat.ToArray().AsChartValues();
                List<int> tempList = Stat.Select(x => x.CountHours).ToList();
                CountHoursTrack = new ChartValues<int>(tempList);
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при выборе задания. Смотрите журнал логирования");
                Log.Error("Ошибка при выборе задания - " + ex.Message);
            }
        }

        public void LoadPdfFileCommandExecute(object obj)
        {
            try
            {
                if (SelectedTask == null)
                {
                    _notifier.ShowWarning("Выберите задание!");
                    return;
                }
                List<StatisticTrackDTO> Stat = new List<StatisticTrackDTO>();
                foreach (var status in LabelsTrack)
                {
                    StatisticTrackDTO temp = new StatisticTrackDTO();
                    temp.NameStatus = status;
                    temp.CountHours = _trackService.GetSumHours(SelectedTask.Id, status);
                    Stat.Add(temp);
                }

                string header = "Отчёт о количестве затраченных часов\n Название:\n" + SelectedTask.Name;
                _loadFileService.SaveStatisitcForCurrentTask("TaskReport.pdf", Stat, header);
                _notifier.ShowSuccess("Отчёт создан в PDF");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при выгрузке отчёта по треккингу времени в PDF");
                Log.Error("Ошибка при выгрузке отчёта по треккингу времени в PDF - " + ex.Message);
            }
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

        public Func<double, string> Formatter { get; set; }

        public TrackVM(ITrackService trackService, ITaskService taskService, ILoadFileService loadFileService)
        {
            _trackService = trackService;
            _taskService = taskService;
            _loadFileService = loadFileService;

            LabelsTrack = new ObservableCollection<string>(new List<string>() { "Plan", "InProgress", "Review", "Stage", "Test" });
            Tasks = new ObservableCollection<TaskDTO>(_taskService.GetTasks());
            CountHoursTrack = new ChartValues<int>(new List<int> { 100, 100, 50, 24, 1 });
            Formatter = value => (value / 24.0 <= 1.0 || value <= 0) ? value + " часов" : (int)(value / 24) + " дней, " + (int)(value % 24) + " часов";
            YAxis = 0;

            ChoiceTask = new RelayCommand(ChoiceTaskExecute);

            LoadPdfFileCommand = new RelayCommand(LoadPdfFileCommandExecute);

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
