using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PagesVM : ViewModelBase
    {
        IPageService _pageService;

        InfSectionDTO _currentInfSection;

        private ObservableCollection<PageDTO> _pages;
        public ObservableCollection<PageDTO> Pages
        {
            get { return _pages; }
            set { _pages = value; OnPropertyChanged(); }
        }

        private PageDTO _selected_page;
        public PageDTO SelectedPage
        {
            get { return _selected_page; }
            set { _selected_page = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private PageAddMenu _addMenu;

        private void AddCommandExecute(object obj)
        {
            _addMenu = new PageAddMenu(_currentInfSection);
            _addMenu.ShowDialog();

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(_currentInfSection.Id));
        }

        private void DeleteCommandExecute(object obj)
        {
            _pageService.DeletePage(_currentInfSection.Id);

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(_currentInfSection.Id));
        }

        public PagesVM(IPageService pageService, InfSectionDTO currentInfSection) 
        {
            _pageService = pageService;
            _currentInfSection = currentInfSection;

            AddCommand = new RelayCommand(AddCommandExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute);

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPages());
        }
    }
}
