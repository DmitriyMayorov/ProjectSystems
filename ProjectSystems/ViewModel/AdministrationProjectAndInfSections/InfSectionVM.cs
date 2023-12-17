using BLL.DTO;
using BLL.Interfaces;
using Microsoft.Win32.SafeHandles;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class InfSectionVM : ViewModelBase
    {
        IInfSerctionService _infSerctionService;
        IPageService _pageService;

        ProjectDTO _projectDTO;
        InfSectionAddMenu _infSectionMenu;

        private ObservableCollection<InfSectionDTO> _sections;
        public ObservableCollection<InfSectionDTO> Sections
        {
            get { return _sections; }
            set { _sections = value; OnPropertyChanged(); }
        }

        private InfSectionDTO _selected_section;
        public InfSectionDTO SelectedSection
        {
            get { return _selected_section; }
            set { _selected_section = value; OnPropertyChanged(); }
        }

        public ICommand AddInfSectionCommand {  get; set; }
        public ICommand UpdateInfSectionCommand { get; set; }
        public ICommand RemoveInfSectionCommand { get; set; }

        private void AddInfSectionCommandExecute(object obj)
        {
            _infSectionMenu = new InfSectionAddMenu(_projectDTO);
            _infSectionMenu.ShowDialog();
            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());
        }

        private void UpdateInfSectionCommandExecute(object obj)
        {
            _infSerctionService.UpdateInfSection(SelectedSection);
        }

        private void RemoveInfSectionCommandExecute(object obj)
        {
            if (SelectedSection == null)
                return;
            _infSerctionService.DeleteInfSection(SelectedSection.Id);
            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());
        }

        public InfSectionVM(IInfSerctionService infSerctionService, IPageService pageService, ProjectDTO projectDTO)
        {
            _infSerctionService = infSerctionService;
            _pageService = pageService;
            _projectDTO = projectDTO;

            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());

            AddInfSectionCommand = new RelayCommand(AddInfSectionCommandExecute);
            UpdateInfSectionCommand = new RelayCommand(UpdateInfSectionCommandExecute);
            RemoveInfSectionCommand = new RelayCommand(RemoveInfSectionCommandExecute);
        }
    }
}
