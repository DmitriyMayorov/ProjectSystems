using BLL.DTO;
using BLL.Interfaces;
using Npgsql;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Serilog;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PageCurrentVM : ViewModelBase
    {
        IPageService _pageService;

        private Notifier _notifier;

        PageDTO _pageDTO;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged();}
        }

        public ICommand UpdateCommand { get; set; }

        private void UpdateCommandExecute(object obj)
        {
            try
            {
                _pageDTO.Name = Name;
                _pageDTO.TextSection = Description;
                _pageService.UpdatePage(_pageDTO);
                _notifier.ShowSuccess("Успешное обновление данных");
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при обновлении данных информационной страницы. Смотрите журнал логирования");
                Log.Error("Ошибка при обновлении данных инфомационных страниц - " +  ex.Message);
            }
        }

        public PageCurrentVM(IPageService pageService, PageDTO pageDTO) 
        {
            _pageDTO = pageDTO;
            _pageService = pageService;

            UpdateCommand = new RelayCommand(UpdateCommandExecute);

            Name = _pageDTO.Name;
            Description = _pageDTO.TextSection;

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
