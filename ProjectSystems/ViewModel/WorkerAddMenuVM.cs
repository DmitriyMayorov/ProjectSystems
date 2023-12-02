﻿using BLL.DTO;
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

namespace ProjectSystems.ViewModel
{
    public class WorkerAddMenuVM : ViewModelBase
    {
        IWorkerService _workerService;
        IPositionService _positionService;

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
            set { _selected_position = value; OnPropertyChanged();}
        }

        private void AddCommandExecute(object obj)
        {
            try
            {
                WorkerDTO temp = new WorkerDTO();
/*                temp.Id = _workerService.GetWorkers().Count() == 0 ? 1 : _workerService.GetWorkers().Max(x => x.Id) + 1;*/
                temp.Person = _fio;
                temp.PassportNum = Int32.Parse(_passport_num);
                temp.PassportSeries = Int32.Parse(_passport_series);
                temp.IDPosition = SelectedPosition.Id;
                temp.Position = SelectedPosition;

                _workerService.CreateWorker(temp);

                MessageBox.Show("Успешно добавлен работник!");
            }
            catch(Exception ex)
            {
            
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Данные не кореектны");
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
        }
    }
}