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

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TrackAddVM : ViewModelBase
    {
        ITrackService _trackService;
        TaskDTO _taskDTO;

        private string selected_count_hours;
        public string SelectedCountHours
        {
            get { return selected_count_hours; }
            set { selected_count_hours = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
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
                case "Ready": MessageBox.Show("Нельзя добавлять время в выполненные задания"); return;
                default:
                    MessageBox.Show("Ошибка!");
                    return;
            }
            temp.IDTask = _taskDTO.Id;
            temp.CountHours = Int32.Parse(SelectedCountHours);

            _trackService.CreateTrack(temp);
            MessageBox.Show("Успешое добавление");
        }

        public TrackAddVM(ITrackService trackService, TaskDTO taskDTO)
        {
            _trackService = trackService; 
            _taskDTO = taskDTO;

            AddCommand = new RelayCommand(AddCommandExecute);
        }
    }
}
