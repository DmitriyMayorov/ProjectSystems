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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Serilog;
using ToastNotifications.Messages;

namespace ProjectSystems.ViewModel
{
    public class ProjectsVM : ViewModelBase
    {
        private IProjectService _projectService;

        private Notifier _notifier;

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
            try
            {
                foreach (ProjectDTO project in Projects)
                {
                    _projectService.UpdateProject(project);
                }
                _notifier.ShowSuccess("Успешное обновление информации о проектах");
            }
            catch (Exception ex)
            {
                _notifier.ShowSuccess("Ошибка при обновлении инфомации о проектах. Смотрите журнал логирования");
                Log.Error("Ошибка при обновлении инфомации о проектах - " + ex.Message);
            }
        }

        private void RemoveProjectExecute(object obj)
        {
            try
            {
                _projectService.DeleteProject(SelectedProject.Id);
                Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
                _notifier.ShowSuccess("Успешное удаление проекта");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Общика при удалении проекта. Смотрите журнал логирования");
                Log.Error("Ошибка при удалении проекта - " + ex.Message);
            }
        }

        public ProjectsVM(IProjectService projectService)
        {
            _projectService = projectService;
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());

            AddProject = new RelayCommand(AddProjectExecute);
            UpdateProjects = new RelayCommand(UpdateProjectsExecute);
            RemoveProject = new RelayCommand(RemoveProjectExecute);

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: System.Windows.Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
            });
        }
    }
}
