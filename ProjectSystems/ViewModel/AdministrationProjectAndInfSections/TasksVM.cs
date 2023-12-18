using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TasksVM : ViewModelBase
    {
        ITaskService _taskService;
        ITrackService _trackService;
        IMessageService _messageService;

        private Notifier _notifier;

        ProjectDTO _projectDTO;
        TaskAddMenu addMenu;

        private ObservableCollection<TaskDTO> _tasks;
        public ObservableCollection<TaskDTO> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; OnPropertyChanged(); }
        }

        private TaskDTO _selected_task;
        public TaskDTO SelectedTask
        {
            get { return _selected_task; }
            set { _selected_task = value; OnPropertyChanged(); }
        }

        public ICommand AddTask { get; set; }
        public ICommand UpdateTask { get; set; }
        public ICommand RemoveTask { get; set; }

        public void AddTaskExecute(object obj)
        {
            addMenu = new TaskAddMenu(_projectDTO);
            addMenu.ShowDialog();
            Tasks = _projectDTO == null ? new ObservableCollection<TaskDTO>() : new ObservableCollection<TaskDTO>(_taskService.GetTasksByProjectID(_projectDTO.Id));
        }

        private void UpdateCommandExecute(object obj)
        {
            foreach (TaskDTO task in Tasks)
            {
                _taskService.UpdateTask(task);
            }
        }

        private void RemoveCommandExecute(object obj)
        {
            _taskService.DeleteTask(SelectedTask.Id);
            Tasks = _projectDTO == null ? new ObservableCollection<TaskDTO>() : new ObservableCollection<TaskDTO>(_taskService.GetTasksByProjectID(_projectDTO.Id));
        }

        public TasksVM(ITaskService taskService, ITrackService trackService, IMessageService messageService, ProjectDTO project)
        {
            _taskService = taskService;
            _trackService = trackService;
            _messageService = messageService;

            _projectDTO = project;

            Tasks = _projectDTO == null ? new ObservableCollection<TaskDTO>() : new ObservableCollection<TaskDTO>(_taskService.GetTasksByProjectID(_projectDTO.Id));

            AddTask = new RelayCommand(AddTaskExecute);
            UpdateTask = new RelayCommand(UpdateCommandExecute);
            RemoveTask = new RelayCommand(RemoveCommandExecute);

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
