using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PageAddVM : ViewModelBase
    {
        IPageService _pageService;
        InfSectionDTO _infSection;

        private string _selected_name;
        public string SelectedName
        {
            get { return _selected_name; }
            set { _selected_name = value; OnPropertyChanged(); }
        }

        private string _selected_description;
        public string SelectedDescription
        {
            get { return _selected_description; }
            set { _selected_description = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            if (SelectedName == null || SelectedName == "" ||
                SelectedDescription == null || SelectedDescription == "")
                return;
            PageDTO temp = new PageDTO();
            temp.Name = SelectedName;
            temp.TextSection = SelectedDescription;
            temp.IDSection = _infSection.Id;

            _pageService.CreatePage(temp);
            MessageBox.Show("Успешное добавление");
        }

        public PageAddVM(IPageService pageService, InfSectionDTO infSectionDTO)
        {
            _pageService = pageService;
            _infSection = infSectionDTO;

            AddCommand = new RelayCommand(AddCommandExecute);
        }
    }
}
