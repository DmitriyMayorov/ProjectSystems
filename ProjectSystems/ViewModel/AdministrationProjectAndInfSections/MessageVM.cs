﻿using BLL.DTO;
using BLL.Interfaces;
using Microsoft.Xaml.Behaviors.Media;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MessageVM : ViewModelBase
    {
        IMessageService _messageService;
        ITaskService _taskService;
        Notifier _notifier;

        private string _status;

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

        private ObservableCollection<MessageDTO> _messages;
        public ObservableCollection<MessageDTO> Messages
        {
            get { return _messages; }
            set {  _messages = value; OnPropertyChanged(); }
        }

        private string _new_message_text;
        public string NewMessageText
        {
            get { return _new_message_text; }
            set { _new_message_text = value; OnPropertyChanged(); }
        }

        public ICommand ChoiseCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private void ChoiseCommandExecute(object obj)
        {
            try
            {
                if (SelectedTask == null)
                {
                    _notifier.ShowWarning("Выберите нужное задание");
                    return;
                }
                Messages = new ObservableCollection<MessageDTO>(_messageService.GetMessagesForCurrentTask(SelectedTask.Id));
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при загрузке чата для нужного задания в проекте. Смотрите журнал логирования");
                Log.Error("Ошибка при загрузке чата для нужного задания в проекте - " +  ex.Message);
            }
        }

        public void AddCommandExecute(object obj)
        {
            try
            {
                if (SelectedTask == null)
                {
                    _notifier.ShowWarning("Выберите нужное задание");
                    return;
                }
                MessageDTO temp = new MessageDTO();
                temp.TextMessage = NewMessageText;
                temp.DateMessage = DateTime.Now;
                switch (SelectedTask.State)
                {
                    case "Plan": temp.IDWorker = (int)SelectedTask.IDWorkerAnalyst; break;
                    case "InProgress": temp.IDWorker = (int)SelectedTask.IDWorkerCoder; break;
                    case "CodeRewiew": temp.IDWorker = (int)SelectedTask.IDWorkerMentor; break;
                    case "Stage": temp.IDWorker = (int)SelectedTask.IDWorkerCoder; break;
                    case "Test": temp.IDWorker = (int)SelectedTask.IDWorkerTester; break;
                    case "Ready": _notifier.ShowError("Нельзя добавлять сообщения в выполненные задания"); return;
                    default:
                        _notifier.ShowError("Ошибка!");
                        return;
                }
                temp.IDTask = SelectedTask.Id;

                _messageService.CreateMessage(temp);

                Messages = new ObservableCollection<MessageDTO>(_messageService.GetMessagesForCurrentTask(SelectedTask.Id));
                NewMessageText = "";
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при добавлении нового сообщения. Смотрите журнал логирования");
                Log.Error("Ошибка при добавлении нового сообщения - " + ex.Message);
            }
        }

        public MessageVM(ITaskService taskService, IMessageService messageService, ProjectDTO project, string status)
        {
            _taskService = taskService; 
            _messageService = messageService;
            _status = status;

            Tasks = new ObservableCollection<TaskDTO>(taskService.GetTaskByStatusTaskFromCurrentProject(project, _status));

            AddCommand = new RelayCommand(AddCommandExecute);
            ChoiseCommand = new RelayCommand(ChoiseCommandExecute);

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
