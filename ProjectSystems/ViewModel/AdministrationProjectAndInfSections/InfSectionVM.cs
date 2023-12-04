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
        PageAddMenu _pageAddMenu;

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

        private ObservableCollection<PageDTO> _pages;
        public ObservableCollection<PageDTO> Pages
        {
            get { return _pages; }
            set { _pages = value; OnPropertyChanged(); }
        }

        private PageDTO _selected_pages;
        public PageDTO SelectedPages
        {
            get { return _selected_pages; }
            set { _selected_pages = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ChoiseCommand { get; set; }
        public ICommand AddInfSectionCommand {  get; set; }
        public ICommand UpdateInfSectionCommand { get; set; }
        public ICommand RemoveInfSectionCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            if (SelectedSection == null)
                return;
            _pageAddMenu = new PageAddMenu(SelectedSection);
            _pageAddMenu.ShowDialog();

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(SelectedSection.Id));
        }

        private void UpdateCommandExecute(object obj) 
        {
            if (SelectedSection == null)
                return;
            foreach (var page in Pages)
            {
                _pageService.UpdatePage(page);
            }
        }

        private void RemoveCommandExecute(object obj) 
        {
            if (SelectedSection == null)
                return;
            _pageService.DeletePage(SelectedSection.Id);
        }

        private void ChoiseCommandExecute(object obj)
        {
            if (SelectedSection == null)
                return;
            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(SelectedSection.Id));
        }

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
/*            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(SelectedSection.Id));*/
        }

        public InfSectionVM(IInfSerctionService infSerctionService, IPageService pageService, ProjectDTO projectDTO)
        {
            _infSerctionService = infSerctionService;
            _pageService = pageService;
            _projectDTO = projectDTO;

            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());

            AddCommand = new RelayCommand(AddCommandExecute);
            UpdateCommand = new RelayCommand(UpdateCommandExecute);
            RemoveCommand = new RelayCommand(RemoveCommandExecute);
            ChoiseCommand = new RelayCommand(ChoiseCommandExecute);
            AddInfSectionCommand = new RelayCommand(AddInfSectionCommandExecute);
            UpdateInfSectionCommand = new RelayCommand(UpdateInfSectionCommandExecute);
            RemoveInfSectionCommand = new RelayCommand(RemoveInfSectionCommandExecute);
        }
    }
}
