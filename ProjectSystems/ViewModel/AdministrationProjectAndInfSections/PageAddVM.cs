using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PageAddVM : ViewModelBase
    {
        IPageService _pageService;
        InfSectionDTO _infSection;
        Notifier _notifier;

        private string _selected_name;
        public string SelectedName
        {
            get { return _selected_name; }
            set { _selected_name = value; OnPropertyChanged(); }
        }

        private string _selected_description;
        public string SelectedDescription
        {
            get { return _selected_description; }
            set { _selected_description = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }

        private void AddCommandExecute(object obj)
        {
            if (SelectedName == null || SelectedName == "" ||
                SelectedDescription == null || SelectedDescription == "")
            {
                _notifier.ShowError("Не получилось добавить. Выберите название и описание!");
                return;
            }
            PageDTO temp = new PageDTO();
            temp.Name = SelectedName;
            temp.TextSection = SelectedDescription;
            temp.IDSection = _infSection.Id;

            _pageService.CreatePage(temp);
            _notifier.ShowSuccess("Успешное добавление");
        }

        public PageAddVM(IPageService pageService, InfSectionDTO infSectionDTO)
        {
            _pageService = pageService;
            _infSection = infSectionDTO;

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
