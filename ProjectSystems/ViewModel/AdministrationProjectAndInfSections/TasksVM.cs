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
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class TasksVM : ViewModelBase
    {
        ITaskService _taskService;
        ITrackService _trackService;
        IMessageService _messageService;

        private Notifier _notifier;

        public string _status;

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

        private Visibility _visAdd;
        public Visibility VisAdd
        {
            get { return _visAdd; }
            set { _visAdd = value; OnPropertyChanged(); }
        }

        private Visibility _visRemove;
        public Visibility VisRemove
        {
            get { return _visRemove; }
            set { _visRemove = value; OnPropertyChanged(); }
        }

        public ICommand AddTask { get; set; }
        public ICommand UpdateTask { get; set; }
        public ICommand RemoveTask { get; set; }

        public void AddTaskExecute(object obj)
        {
            addMenu = new TaskAddMenu(_projectDTO);
            addMenu.ShowDialog();
            Tasks = new ObservableCollection<TaskDTO>(_taskService.GetTaskByStatusTaskFromCurrentProject(_projectDTO, _status));
        }

        private void RemoveCommandExecute(object obj)
        {
            try
            {
                _taskService.DeleteTask(SelectedTask.Id);
                Tasks = new ObservableCollection<TaskDTO>(_taskService.GetTaskByStatusTaskFromCurrentProject(_projectDTO, _status));
                _notifier.ShowSuccess("Успешное удаление задания");
            }
            catch(Exception ex) 
            {
                _notifier.ShowError("Ошибка при удалении задания. Смотрите журанл логирования");
                Log.Error("Ошибка при удалении задания - " + ex.Message);
            }
        }

        public void GetPersmission(string status)
        {
            if (status == "Analyst" || status == "Tester")
                VisAdd = VisRemove = Visibility.Visible;
            else if (status == "Coder")
                VisAdd = VisRemove = Visibility.Hidden;
            else
            {
                VisAdd = VisRemove = Visibility.Hidden;
                _notifier.ShowError("не правильный статус");
            }
        }

        public TasksVM(ITaskService taskService, ITrackService trackService, IMessageService messageService, ProjectDTO project, string status)
        {
            _taskService = taskService;
            _trackService = trackService;
            _messageService = messageService;

            _projectDTO = project;
            _status = status;

            AddTask = new RelayCommand(AddTaskExecute);
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

            Tasks = new ObservableCollection<TaskDTO>(_taskService.GetTaskByStatusTaskFromCurrentProject(_projectDTO, _status));
            GetPersmission(_status);
        }
    }
}
