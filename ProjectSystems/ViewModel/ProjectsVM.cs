using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSystems.ViewModel
{
    public class ProjectsVM : ViewModelBase
    {
        private IProjectService _projectService;
        private ObservableCollection<ProjectDTO> _projects;
        public ObservableCollection<ProjectDTO> Projects
        {
            get { return _projects; }
            set { _projects = value; OnPropertyChanged(); }
        }

        private ProjectDTO _selected_project;
        public ProjectDTO SelectedProject
        {
            get { return _selected_project; }
            set { _selected_project = value; OnPropertyChanged(); }
        }

        private ProjectAddMenu _menu;

        public ICommand AddProject { get; set; }
        public ICommand UpdateProjects { get; set; }
        public ICommand RemoveProject { get; set; }

        private void AddProjectExecute(object obj)
        {
            _menu = new ProjectAddMenu();
            _menu.ShowDialog();

            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
        }

        private void UpdateProjectsExecute(object obj)
        {
            foreach (ProjectDTO project in Projects)
            {
                _projectService.UpdateProject(project);
            }
        }

        private void RemoveProjectExecute(object obj)
        {
            _projectService.DeleteProject(SelectedProject.Id);
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
        }

        public ProjectsVM(IProjectService projectService)
        {
            _projectService = projectService;
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());

            AddProject = new RelayCommand(AddProjectExecute);
            UpdateProjects = new RelayCommand(UpdateProjectsExecute);
            RemoveProject = new RelayCommand(RemoveProjectExecute);
        }
    }
}
