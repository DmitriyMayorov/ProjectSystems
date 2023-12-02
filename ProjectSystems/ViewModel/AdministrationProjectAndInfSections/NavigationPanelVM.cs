﻿using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class NavigationPanelVM : ViewModelBase
    { 
        private IInfSerctionService _infSerctionService;
        private IMessageService _messageService;
        private IPageService _pageService;
        private IProjectService _projectService;
        private IReportService _reportService;
        private ITaskService _taskService;
        private ITrackService _trackService;

        private object _currentViewPanel;
        public object CurrentViewPanel
        {
            get { return _currentViewPanel; }
            set { _currentViewPanel = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProjectDTO> _projects;
        public ObservableCollection<ProjectDTO> Projects
        {
            get { return _projects; }
            set { _projects = value; OnPropertyChanged(); }
        }

        private ProjectDTO _currentProject;
        public ProjectDTO CurrentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, _currentProject); OnPropertyChanged(); }
        }

        public ICommand InfSectionCommand { get; set; }
        public ICommand TaskCommand { get; set; }
        public ICommand ProjectCommand { get; set; }

        private void InfSection(object obj) => CurrentViewPanel = new InfSectionVM(_infSerctionService, _pageService);
        private void Task(object obj) => CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject);

        public NavigationPanelVM(IInfSerctionService infSectionService, IMessageService messageService, IPageService pageService, IProjectService projectService,
                                 IReportService reportService, ITaskService taskService, ITrackService trackService)
        {
            _infSerctionService = infSectionService;
            _messageService = messageService;
            _pageService = pageService;
            _projectService = projectService;
            _reportService = reportService;
            _taskService = taskService;
            _trackService = trackService;

            InfSectionCommand = new RelayCommand(InfSection);
            TaskCommand = new RelayCommand(Task);

            CurrentProject = _projectService.GetProjects().FirstOrDefault();

            CurrentViewPanel = new TasksVM(_taskService, _trackService, _messageService, CurrentProject);
            Projects = new ObservableCollection<ProjectDTO>(_projectService.GetProjects());
        }
    }
}