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

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class StatisticTrack
    {
        public string NameStatus;
        public int CountHours;
    }

    public class TrackVM : ViewModelBase
    {
        ITrackService _trackService;
        ITaskService _taskService;

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

        public void ChoiceTaskExecute(object obj)
        {
            if (SelectedTask == null)
                return;
            List<StatisticTrack> Stat = new List<StatisticTrack>();
            foreach (var status in LabelsTrack)
            {
                StatisticTrack temp = new StatisticTrack();
                temp.NameStatus = status;
                temp.CountHours = _trackService.GetSumHours(SelectedTask.Id, status);

                Stat.Add(temp);
            }
            ResultsTrack = Stat.ToArray().AsChartValues();
            List<int> tempList = Stat.Select(x => x.CountHours).ToList();
            CountHoursTrack = new ChartValues<int>(tempList);
        }

        public void AddCommandExecute(object obj)
        {
            if (SelectedTask == null)
                return;
            addMenu = new TrackAddMenu(SelectedTask);
            addMenu.ShowDialog();

            List<StatisticTrack> Stat = new List<StatisticTrack>();
            foreach (var status in LabelsTrack)
            {
                StatisticTrack temp = new StatisticTrack();
                temp.NameStatus = status;
                temp.CountHours = _trackService.GetSumHours(SelectedTask.Id, status);

                Stat.Add(temp);
            }
            ResultsTrack = Stat.ToArray().AsChartValues();
            List<int> tempList = Stat.Select(x => x.CountHours).ToList();
            CountHoursTrack = new ChartValues<int>(tempList);
        }

        private ChartValues<StatisticTrack> _result;
        public ChartValues<StatisticTrack> ResultsTrack 
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

        public TrackVM(ITrackService trackService, ITaskService taskService)
        {
            _trackService = trackService;
            _taskService = taskService;

            LabelsTrack = new ObservableCollection<string>(new List<string>() { "Plan", "InProgress", "CodeRewiew", "Stage", "Test" });
            Tasks = new ObservableCollection<TaskDTO>(_taskService.GetTasks());
            ChoiceTask = new RelayCommand(ChoiceTaskExecute);
            CountHoursTrack = new ChartValues<int>(new List<int> {100, 100, 50, 24, 10 });
            Formatter = value => (value / 24.0 <= 1.0 || value <= 0) ? value + " часов" : (int)(value / 24) + " дней, " + (int)(value % 24) + " часов";
            YAxis = 0;

            AddCommand = new RelayCommand(AddCommandExecute);
        }
    }
}
