using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View;
using ProjectSystems.ViewModel.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Serilog;
using ToastNotifications.Messages;

namespace ProjectSystems.ViewModel
{
    public class WorkersVM : ViewModelBase
    {
        private IWorkerService workerService;
        private IPositionService positionService;

        private Notifier _notifier;

        private  ObservableCollection<WorkerDTO> _workersDTO;
        public ObservableCollection<WorkerDTO> Workers
        {
            get { return _workersDTO; }
            set { _workersDTO = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PositionDTO> TEST { get; set; } 

        private ObservableCollection<PositionDTO> _positions;
        public ObservableCollection<PositionDTO> Positions
        {
            get { return _positions; }
            set { _positions = value; OnPropertyChanged(); }
        }

        private WorkerDTO _selectedWorker;
        public WorkerDTO SelectedWorker
        {
            get { return _selectedWorker; }
            set { _selectedWorker = value; OnPropertyChanged(); }
        }

        private WorkerAddMenu AddMenu;

        public ICommand AddWorker { get; set; }
        public ICommand RemoveWorker {  get; set; }
        public ICommand UpdateWorker { get; set; }
        
        private void AddWorkerMenu(object obj)
        {
            AddMenu = new WorkerAddMenu(workerService, positionService);
            AddMenu.ShowDialog();

            Workers = new ObservableCollection<WorkerDTO>(workerService.GetWorkers());
        }

        private void RemoveWorkerExecute(object obj)
        { 
            try 
            {
                workerService.DeleteWorker(SelectedWorker.Id);
                Workers = new ObservableCollection<WorkerDTO>(workerService.GetWorkers());
                _notifier.ShowSuccess("Успешное удаление сотрудника");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при удалении сотрудника. Смотрите журнал логирования");
                Log.Error("Ошибка при удалении сотрдуника - " + ex.Message);
            }
        }

        private void UpdateWorkerExexute(object obj)
        {
            try
            {
                foreach (WorkerDTO worker in Workers)
                {
                    workerService.UpdateWorker(worker);
                }
                _notifier.ShowSuccess("Успешное обновление информации о сотрдуниках");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при обновлении информации о сотрудниках. Смотрите журанл логирования");
                Log.Error("Ошибка при обновлении информации о сотрудниках - " + ex.Message);
            }
        }

        public WorkersVM(IWorkerService workerService, IPositionService positionService)
        {
            this.workerService = workerService;
            this.positionService = positionService;

            TEST = new ObservableCollection<PositionDTO>(positionService.GetPositions());
            Workers = new ObservableCollection<WorkerDTO>(workerService.GetWorkers());
            
            AddWorker = new RelayCommand(AddWorkerMenu);
            RemoveWorker = new RelayCommand(RemoveWorkerExecute);
            UpdateWorker = new RelayCommand(UpdateWorkerExexute);

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: System.Windows.Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
            });
        }
    }
}
