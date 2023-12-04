using BLL.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class ReportsVM : ViewModelBase
    {
        IReportService _reportService;
        ILoadFileService _loadFileService;

        Notifier _notifier;

        public double YAxis { get; set; }

        private DateTime _startDate;
        public DateTime StartDate
        { 
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); } 
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _countHours;
        public ChartValues<int> CountHoursTrack
        {
            get { return _countHours; }
            set { _countHours = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _labelTrack;
        public ObservableCollection<string> LabelsTrack
        {
            get { return _labelTrack; }
            set { _labelTrack = value; OnPropertyChanged(); }
        }

        public Func<double, string> Formatter { get; set; }

        public SeriesCollection Series {  get; set; }

        public ICommand ChoiseCommand { get; set; }
        public ICommand LoadPdfFileCommand {  get; set; }

        private void ChoiseCommandExecute(object obj)
        {
            if (StartDate == null || EndDate == null)
                return;
            var temp = _reportService.GetStatisticByAllPerson(StartDate, EndDate);
            CountHoursTrack = new ChartValues<int>(temp.Select(i => (int)i.CountHours).ToList());
            LabelsTrack = new ObservableCollection<string>(temp.Select(i => i.Person.ToString()).ToList());

            Series.Clear();
            foreach(var item in temp)
            {
                Series.Add(new PieSeries()
                {
                    Title = item.Person,
                    DataLabels = true,
                    Values = new ChartValues<ObservableValue>() { new ObservableValue(item.CountCompletedTasks) }
                });
            }
        }

        private void LoadPdfFileCommandExecute(object obj)
        {
            if (StartDate == null || EndDate == null)
            {
                _notifier.ShowError("Выберите временной промежуток!");
                return;
            }
            string header = "Отчёт по эффективности работы сотрудников \nза период между " + StartDate.Date.ToString() + " и " + EndDate.Date.ToString() + "\n";
            _loadFileService.SaveStatisticForAllPerson("StatisticWorker.pdf", _reportService.GetStatisticByAllPerson(StartDate, EndDate), header);
            _notifier.ShowSuccess("Отчёт создан в PDF");
        }

        public ReportsVM(IReportService reportService, ILoadFileService loadFileService)
        {
            _reportService = reportService;
            _loadFileService = loadFileService;

            ChoiseCommand = new RelayCommand(ChoiseCommandExecute);
            LoadPdfFileCommand = new RelayCommand(LoadPdfFileCommandExecute);

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            YAxis = 0;
            CountHoursTrack = new ChartValues<int>();
            LabelsTrack = new ObservableCollection<string>();
            Formatter = value => (value / 24.0 <= 1.0 || value <= 0) ? value + " часов" : (int)(value / 24) + " дней, " + (int)(value % 24) + " часов";

            Series = new SeriesCollection();
            _loadFileService = loadFileService;

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
