using BLL.DTO;
using BLL.Interfaces;
using Npgsql;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PageCurrentVM : ViewModelBase
    {
        IPageService _pageService;

        PageDTO _pageDTO;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged();}
        }

        public ICommand UpdateCommand { get; set; }

        private void UpdateCommandExecute(object obj)
        {
            _pageDTO.Name = Name;
            _pageDTO.TextSection = Description;
            _pageService.UpdatePage(_pageDTO);
        }

        public PageCurrentVM(IPageService pageService, PageDTO pageDTO) 
        {
            _pageDTO = pageDTO;
            _pageService = pageService;

            UpdateCommand = new RelayCommand(UpdateCommandExecute);

            Name = _pageDTO.Name;
            Description = _pageDTO.TextSection;
        }
    }
}
