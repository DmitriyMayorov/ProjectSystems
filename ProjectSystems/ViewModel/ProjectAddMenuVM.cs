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

namespace ProjectSystems.ViewModel
{
    public class ProjectAddMenuVM : ViewModelBase
    {
        IProjectService _projectService;

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; OnPropertyChanged(); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand {  get; set; }

        private void AddProjectExecute(object obj)
        {
            ProjectDTO temp = new ProjectDTO();
            temp.Name = ProjectName;
            temp.DeadLine = Date;

            _projectService.CreateProject(temp);
            MessageBox.Show("Успешное добавление проекта");
        }

        public ProjectAddMenuVM(IProjectService projectService)
        {
            _projectService = projectService;
            AddCommand = new RelayCommand(AddProjectExecute);
        }
    }
}
