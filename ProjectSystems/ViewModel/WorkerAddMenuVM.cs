using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectSystems.ViewModel
{
    public class WorkerAddMenuVM : ViewModelBase
    {
        IWorkerService _workerService;
        IPositionService _positionService;

        Notifier _notifier;

        private string _fio;
        public string FIO
        {
            get { return _fio; }
            set { _fio = value; OnPropertyChanged(); }
        }

        private string _passport_num;
        public string PassportNum
        {
            get { return _passport_num; }
            set { _passport_num = value; OnPropertyChanged(); }
        }

        private string _passport_series;
        public string PassportSeries
        {
            get { return _passport_series; }
            set { _passport_series = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PositionDTO> _positionsDTO;
        public ObservableCollection<PositionDTO> PositionsDTO
        {
            get { return _positionsDTO; }
            set { _positionsDTO = value; OnPropertyChanged(); }
        }

        private PositionDTO _selected_position;
        public PositionDTO SelectedPosition
        {
            get { return _selected_position; }
            set { _selected_position = value; OnPropertyChanged(); }
        }

        private void AddCommandExecute(object obj)
        {
            try
            {
                if (FIO == null || PassportNum == null ||
                    PassportSeries == null || SelectedPosition == null)
                {
                    _notifier.ShowWarning("Некорректные данные");
                    return;
                }
                int tempResult = 0;
                if (!Int32.TryParse(PassportNum,out tempResult) || !Int32.TryParse(PassportSeries, out tempResult))
                {
                    _notifier.ShowWarning("Некорректные данные");
                    return;
                }
                WorkerDTO temp = new WorkerDTO();
                temp.Person = _fio;
                temp.PassportNum = Int32.Parse(_passport_num);
                temp.PassportSeries = Int32.Parse(_passport_series);
                temp.IDPosition = SelectedPosition.Id;
                temp.Position = SelectedPosition;

                _workerService.CreateWorker(temp);

                _notifier.ShowSuccess("Успешно добавлен работник!");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при добавлении работника. Смотрите журанл логирования");
                Log.Error("Ошибка при добавлении работника - " + ex.Message);
            }
        }

        public ICommand AddCommand { get; set; }

        public WorkerAddMenuVM(IWorkerService workerService, IPositionService positionService)
        {
            _workerService = workerService;
            _positionService = positionService;
            PositionsDTO = new ObservableCollection<PositionDTO>(_positionService.GetPositions());
            SelectedPosition = _positionService.GetPositions().FirstOrDefault();

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
