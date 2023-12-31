﻿using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using ScottPlot.Renderable;
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
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class InfSectionAddVM : ViewModelBase
    {
        IInfSerctionService _infSectionService;
        ProjectDTO _projectDTO;
        Notifier _notifier;

        private string _selected_name;
        public string SelectedName
        {
            get { return _selected_name; }
            set { _selected_name = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            try
            {
                if (SelectedName == null || SelectedName == "")
                {
                    Log.Warning("Добавление информационной секции отменено. Не введено либо название, либо описание секции");
                    _notifier.ShowWarning("Не удалось добавить. Выберите название!");
                    return;
                }
                InfSectionDTO temp = new InfSectionDTO();
                temp.Name = SelectedName;
                temp.IDProject = _projectDTO.Id;

                _infSectionService.CreateInfSection(temp);
                Log.Information("Добавление информационной секции. Название - " + SelectedName);
                _notifier.ShowSuccess("Успешное добавление");
            }
            catch (Exception ex)
            {
                _notifier.ShowError("Ошибка при создании информационной секции. Смотрите журанл логирования");
                Log.Error("Ошибка при создании информационной секции - " + ex.Message);
            }
        }

        public InfSectionAddVM(IInfSerctionService infSerctionService, ProjectDTO projectDTO)
        {
            _infSectionService = infSerctionService;
            _projectDTO = projectDTO;

            AddCommand = new RelayCommand(AddCommandExecute);
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
