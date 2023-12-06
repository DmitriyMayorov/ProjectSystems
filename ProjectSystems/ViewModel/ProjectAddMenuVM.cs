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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ProjectSystems.ViewModel
{
    public class ProjectAddMenuVM : ViewModelBase
    {
        IProjectService _projectService;
        Notifier _notifier;

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
            if (ProjectName == "" || ProjectName == null)
            {
                _notifier.ShowError("Некорректные данные");
                return;
            }
            ProjectDTO temp = new ProjectDTO();
            temp.Name = ProjectName;
            temp.DeadLine = Date;

            _projectService.CreateProject(temp);
            _notifier.ShowSuccess("Успешное добавление проекта");
        }

        public ProjectAddMenuVM(IProjectService projectService)
        {
            _projectService = projectService;
            AddCommand = new RelayCommand(AddProjectExecute);

            Date = DateTime.Now;

            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
