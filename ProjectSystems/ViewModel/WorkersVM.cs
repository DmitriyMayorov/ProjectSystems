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

namespace ProjectSystems.ViewModel
{
    public class WorkersVM : ViewModelBase
    {
        private IWorkerService workerService;
        private IPositionService positionService;

        private  ObservableCollection<WorkerDTO> _workersDTO;
        public ObservableCollection<WorkerDTO> Workers
        {
            get { return _workersDTO; }
            set { _workersDTO = value; OnPropertyChanged(); }
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateWorkerExexute(object obj)
        {
            foreach(WorkerDTO worker in Workers)
            {
                workerService.UpdateWorker(worker);
            }
        }

        public WorkersVM(IWorkerService workerService, IPositionService positionService)
        {
            this.workerService = workerService;
            this.positionService = positionService;

            Workers = new ObservableCollection<WorkerDTO>(workerService.GetWorkers());

            AddWorker = new RelayCommand(AddWorkerMenu);
            RemoveWorker = new RelayCommand(RemoveWorkerExecute);
            UpdateWorker = new RelayCommand(UpdateWorkerExexute);

/*            this.positionService = positionService;*/
        }
    }
}
