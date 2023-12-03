using BLL.DTO;
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

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class MessageVM : ViewModelBase
    {
        IMessageService _messageService;
        ITaskService _taskService;

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
            if (SelectedTask == null)
                return;
            Messages = new ObservableCollection<MessageDTO>(_messageService.GetMessagesForCurrentTask(SelectedTask.Id));
        }

        public void AddCommandExecute(object obj)
        {
            if (SelectedTask == null)
                return;
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
                case "Ready": MessageBox.Show("Нельзя добавлять сообщения в выполненные задания"); return;
                default:
                    MessageBox.Show("Ошибка!");
                    return;
            }
            temp.IDTask = SelectedTask.Id;

            _messageService.CreateMessage(temp);

            Messages = new ObservableCollection<MessageDTO>(_messageService.GetMessagesForCurrentTask(SelectedTask.Id));
            NewMessageText = "";
        }

        public MessageVM(ITaskService taskService, IMessageService messageService)
        {
            _taskService = taskService; 
            _messageService = messageService;

            Tasks = new ObservableCollection<TaskDTO>(taskService.GetTasks());

            AddCommand = new RelayCommand(AddCommandExecute);
            ChoiseCommand = new RelayCommand(ChoiseCommandExecute);
        }
    }
}
