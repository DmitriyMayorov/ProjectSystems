﻿using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
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
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Serilog;

using Dialog = System.Windows.Forms;

namespace ProjectSystems.ViewModel
{
    public class ReportsVM : ViewModelBase
    {
        IProjectService _projectService;
        IReportService _reportService;
        ILoadFileService _loadFileService;

        Notifier _notifier;
        
        public double YAxis { get; set; }

        //Описание состяний на первую вкладку TabControl
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

        private Visibility _visDiagram = Visibility.Hidden;
        public Visibility VisDiagram
        {
            get { return _visDiagram; }
            set { _visDiagram = value; OnPropertyChanged(); }
        }

        public Func<double, string> Formatter { get; set; }

        public SeriesCollection Series {  get; set; }

        //Описание второй вкладки TabControl
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
            set { _currentProject = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _countTasksForCurrentProjectSortByState;
        public ChartValues<int> CountTasksForCurrentProjectSortByState
        {
            get { return _countTasksForCurrentProjectSortByState; }
            set { _countTasksForCurrentProjectSortByState = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _labelsNameSate;
        public ObservableCollection<string> LabelsNameSate
        {
            get { return _labelsNameSate; }
            set { _labelsNameSate = value; OnPropertyChanged(); }
        }

        private Visibility _visDiagramSecond = Visibility.Hidden;
        public Visibility VisDiagramSecond
        {
            get { return _visDiagramSecond; }
            set { _visDiagramSecond = value; OnPropertyChanged(); }
        }

        public Func<double, string> FormatterTask { get; set; }

        public ICommand ChoiseCommand { get; set; }
        public ICommand LoadPdfFileCommand {  get; set; }
        public ICommand ChoiseProjectCommand { get; set; }
        public ICommand LoadPdfFileSecondReportCommand { get; set; }

        private void ChoiseCommandExecute(object obj)
        {
            try
            {
                if (StartDate == null || EndDate == null)
                {
                    _notifier.ShowWarning("Выбрите временной промежуток");
                    return;
                }
                var tempFirstDiagram = _reportService.GetStatisticByAllPerson(StartDate, EndDate);
                CountHoursTrack = new ChartValues<int>(tempFirstDiagram.Select(i => (int)i.CountHours).ToList());
                LabelsTrack = new ObservableCollection<string>(tempFirstDiagram.Select(i => i.Person.ToString()).ToList());

                var tempSecondDiagram = _reportService.GetReportCompletedTasksForWorkers();
                Series.Clear();
                foreach (var item in tempSecondDiagram)
                {
                    Series.Add(new PieSeries()
                    {
                        Title = item.Person,
                        DataLabels = true,
                        Values = new ChartValues<ObservableValue>() { new ObservableValue(item.CompletedTasks) }
                    });
                }
                VisDiagram = Visibility.Visible;
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при выведении статистики по работникам. Смотрите журнал логирования");
                Log.Error("Ошибка при выведении статистики по работникам - ", ex.Message);
            }
        }

        private void ChoiseProjectExecute(object obj)
        {
            try
            {
                if (CurrentProject == null)
                {
                    _notifier.ShowWarning("Выберите проект для статистики");
                    return;
                }
                var result = _reportService.MakeCountTasksForCurrentProjectByStates(CurrentProject);
                CountTasksForCurrentProjectSortByState = new ChartValues<int>(result.Select(i => i.CountTasks).ToList());
                LabelsNameSate = new ObservableCollection<string>(result.Select(i => i.NameState).ToList());
                VisDiagramSecond = Visibility.Visible;
            }
            catch( Exception ex )
            {
                _notifier.ShowError("Ошибка при выведении статистики по заданиям проекта. Смотрите журанл логирования");
                Log.Error("Ошибка при выведении статистики по заданиям проекта - " + ex.Message);
            }
        }

        private void LoadPdfFileCommandExecute(object obj)
        {
            try
            {
                if (StartDate == null || EndDate == null)
                {
                    _notifier.ShowWarning("Выберите временной промежуток!");
                    return;
                }

                Dialog.SaveFileDialog menu = new Dialog.SaveFileDialog();
                menu.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";

                if (menu.ShowDialog() != Dialog.DialogResult.OK)
                    return;
                string pathwithname = menu.FileName;

                string header = "Отчёт по эффективности работы сотрудников \nза период между " + StartDate.Date.ToString() + " и " + EndDate.Date.ToString() + "\n";
                _loadFileService.SaveStatisticForAllPerson(pathwithname, _reportService.GetStatisticByAllPerson(StartDate, EndDate), _reportService.GetReportCompletedTasksForWorkers(), header);
                _notifier.ShowSuccess("Отчёт создан в PDF");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при формировании отчёта по сотрудникам. Смотрите журанл логирования");
                Log.Error("Ошибка при формировании отчёта по сотрудникам - ", ex.Message);
            }
        }

        private void LoadPdfFileSecondReportExecute(object obj)
        {
            try
            {
                if (CurrentProject == null)
                {
                    _notifier.ShowWarning("Выберите проект!");
                    return;
                }

                Dialog.SaveFileDialog menu = new Dialog.SaveFileDialog();
                menu.Filter = "txt files (*.pdf)|*.pdf|All files (*.*)|*.*";

                if (menu.ShowDialog() != Dialog.DialogResult.OK)
                    return;
                string pathwithname = menu.FileName;

                var result = _reportService.MakeCountTasksForCurrentProjectByStates(CurrentProject);
                string header = "Отчёт по состяонию заданий на " + DateTime.Now + "\n" + " для проекта " + CurrentProject.Name;
                _loadFileService.SaveStatisitcForTasksInCurrentProjectByStates(pathwithname, result, header);
                _notifier.ShowSuccess("Отчёт создан в PDF");
            }
            catch( Exception ex)
            {
                _notifier.ShowError("Ошибка при формировании отчёта по заданиям проекта. Смотрите журнал логирвоания");
                Log.Error("Ошибка при формировании отчёта по здааниям проекта - " + ex.Message);
            }
        }

        public ReportsVM(IProjectService projectService, IReportService reportService, ILoadFileService loadFileService)
        {
            _projectService = projectService;
            _reportService = reportService;
            _loadFileService = loadFileService;
            
            //Стартовые значения для первой вкладки
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            YAxis = 1;
            CountHoursTrack = new ChartValues<int>();
            LabelsTrack = new ObservableCollection<string>();
            Series = new SeriesCollection();
            Formatter = value => (value / 24.0 <= 1.0 || value <= 0) ? value + " часов" : (int)(value / 24) + " дней, " + (int)(value % 24) + " часов";

            //Стартовые значения для второй вкладки
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
            CurrentProject = _projectService.GetProjects().FirstOrDefault();
            LabelsNameSate = new ObservableCollection<string>();
            CountTasksForCurrentProjectSortByState = new ChartValues<int>();
            FormatterTask = value => value + " заданий";

            ChoiseCommand = new RelayCommand(ChoiseCommandExecute);
            LoadPdfFileCommand = new RelayCommand(LoadPdfFileCommandExecute);
            ChoiseProjectCommand = new RelayCommand(ChoiseProjectExecute);
            LoadPdfFileSecondReportCommand = new RelayCommand(LoadPdfFileSecondReportExecute);

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
