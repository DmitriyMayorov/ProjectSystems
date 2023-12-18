using BLL.DTO;
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
using Serilog;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PagesVM : ViewModelBase
    {
        IPageService _pageService;

        private Notifier _notifier;

        InfSectionDTO _currentInfSection;

        private ObservableCollection<PageDTO> _pages;
        public ObservableCollection<PageDTO> Pages
        {
            get { return _pages; }
            set { _pages = value; OnPropertyChanged(); }
        }

        private PageDTO _selected_page;
        public PageDTO SelectedPage
        {
            get { return _selected_page; }
            set { _selected_page = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private PageAddMenu _addMenu;

        private void AddCommandExecute(object obj)
        {
            _addMenu = new PageAddMenu(_currentInfSection);
            _addMenu.ShowDialog();

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(_currentInfSection.Id));
        }

        private void DeleteCommandExecute(object obj)
        {
            try
            {
                _pageService.DeletePage(_currentInfSection.Id);

                Pages = new ObservableCollection<PageDTO>(_pageService.GetPagesForCurrentInfSection(_currentInfSection.Id));
                _notifier.ShowInformation("Успешное удаление информационной секции");
            }
            catch (Exception ex)
            {
                _notifier.ShowError("Ошибка при удалении информационной страницы. Смотрите журанл логирования");
                Log.Error("Ошибка при удалении информационной страницы - " + ex.Message);
            }
        }

        public PagesVM(IPageService pageService, InfSectionDTO currentInfSection) 
        {
            _pageService = pageService;
            _currentInfSection = currentInfSection;

            AddCommand = new RelayCommand(AddCommandExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute);

            Pages = new ObservableCollection<PageDTO>(_pageService.GetPages());

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
