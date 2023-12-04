using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class InfSectionAddVM : ViewModelBase
    {
        IInfSerctionService _infSectionService;
        ProjectDTO _projectDTO;

        private string _selected_name;
        public string SelectedName
        {
            get { return _selected_name; }
            set { _selected_name = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            if (SelectedName == null || SelectedName == "")
                return;
            InfSectionDTO temp = new InfSectionDTO();
            temp.Name = SelectedName;
            temp.IDProject = _projectDTO.Id;

            _infSectionService.CreateInfSection(temp);
            MessageBox.Show("Успешное добавление");
        }

        public InfSectionAddVM(IInfSerctionService infSerctionService, ProjectDTO projectDTO)
        {
            _infSectionService = infSerctionService;
            _projectDTO = projectDTO;

            AddCommand = new RelayCommand(AddCommandExecute);
        }
    }
}
