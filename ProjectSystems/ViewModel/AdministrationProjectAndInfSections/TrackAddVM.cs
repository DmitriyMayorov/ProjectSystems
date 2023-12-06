using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TrackAddVM : ViewModelBase
    {
        ITrackService _trackService;
        TaskDTO _taskDTO;

        Notifier _notifier;

        private string selected_count_hours;
        public string SelectedCountHours
        {
            get { return selected_count_hours; }
            set { selected_count_hours = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            if(SelectedCountHours == null || SelectedCountHours == "")
            {
                _notifier.ShowError("Выбрите число!");
                return;
            }
            TrackDTO temp = new TrackDTO();
            temp.DateTrack = DateTime.Now;
            temp.StatusTask = _taskDTO.State;
            switch(temp.StatusTask)
            {
                case "Plan": temp.IDWorker = (int)_taskDTO.IDWorkerAnalyst; break;
                case "InProgress": temp.IDWorker = (int)_taskDTO.IDWorkerCoder; break;
                case "CodeRewiew": temp.IDWorker = (int)_taskDTO.IDWorkerMentor; break;
                case "Stage": temp.IDWorker = (int)_taskDTO.IDWorkerCoder; break;
                case "Test": temp.IDWorker = (int)_taskDTO.IDWorkerTester; break;
                case "Ready": _notifier.ShowError("Нельзя добавлять время в выполненные задания"); return;
                default:
                    _notifier.ShowError("Ошибка!");
                    return;
            }
            temp.IDTask = _taskDTO.Id;
            temp.CountHours = Int32.Parse(SelectedCountHours);

            _trackService.CreateTrack(temp);
            _notifier.ShowSuccess("Успешное добавление");
        }

        public TrackAddVM(ITrackService trackService, TaskDTO taskDTO)
        {
            _trackService = trackService; 
            _taskDTO = taskDTO;

            AddCommand = new RelayCommand(AddCommandExecute);

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
