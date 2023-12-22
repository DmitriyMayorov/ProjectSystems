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
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TrackAddVM : ViewModelBase
    {
        ITrackService _trackService;
        TaskDTO _taskDTO;
        string _status;
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
            try
            {
                if (SelectedCountHours == null || SelectedCountHours == "")
                {
                    _notifier.ShowWarning("Выбрите число!");
                    return;
                }
                TrackDTO temp = new TrackDTO();
                temp.DateTrack = DateTime.Now;
                temp.StatusTask = _taskDTO.State;
                if (!(_trackService.isShouldCreateTask(temp, _status)))
                {
                    _notifier.ShowWarning("Нет прав доступа на добавление времени на таком этапе задания");
                    return;
                }

                switch (temp.StatusTask)
                {
                    case "Plan": temp.IDWorker = (int)_taskDTO.IDWorkerAnalyst; break;
                    case "InProgress": temp.IDWorker = (int)_taskDTO.IDWorkerCoder; break;
                    case "Review": temp.IDWorker = (int)_taskDTO.IDWorkerMentor; break;
                    case "Stage": temp.IDWorker = (int)_taskDTO.IDWorkerCoder; break;
                    case "Test": temp.IDWorker = (int)_taskDTO.IDWorkerTester; break;
                    case "Ready": break;
                    default:
                        _notifier.ShowError("Ошибка!");
                        return;
                }
                temp.IDTask = _taskDTO.Id;
                temp.CountHours = Int32.Parse(SelectedCountHours);

                int flagResult = _trackService.CreateTrack(temp);
                if (flagResult == 0)
                    _notifier.ShowSuccess("Успешное добавление");
                if (flagResult == 1)
                    _notifier.ShowWarning("Нельзя добавлять время в выполненные задания");
                if (flagResult == 2)
                    _notifier.ShowWarning("Нельзя фиксировать отрицательное число часов или 0. Нельзя фиксировать больше 24 часов за день");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при добавлении трекка времени. Смотрите журнал логирования");
                Log.Error("Ошибка при добавлении трекка времени - " + ex.Message);
            }
        }

        public TrackAddVM(ITrackService trackService, TaskDTO taskDTO, string status)
        {
            _trackService = trackService; 
            _taskDTO = taskDTO;
            _status = status;

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
