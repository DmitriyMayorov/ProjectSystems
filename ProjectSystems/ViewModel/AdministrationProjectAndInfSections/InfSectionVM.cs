using BLL.DTO;
using BLL.Interfaces;
using Microsoft.Win32.SafeHandles;
using ProjectSystems.Util.TemplateElement;
using ProjectSystems.View.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications;
using Serilog;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class InfSectionVM : ViewModelBase
    {
        IInfSerctionService _infSerctionService;
        IPageService _pageService;

        private Notifier _notifier;

        ProjectDTO _projectDTO;
        InfSectionAddMenu _infSectionMenu;

        private ObservableCollection<InfSectionDTO> _sections;
        public ObservableCollection<InfSectionDTO> Sections
        {
            get { return _sections; }
            set { _sections = value; OnPropertyChanged(); }
        }

        private InfSectionDTO _selected_section;
        public InfSectionDTO SelectedSection
        {
            get { return _selected_section; }
            set { _selected_section = value; OnPropertyChanged(); }
        }

        public ICommand AddInfSectionCommand {  get; set; }
        public ICommand UpdateInfSectionCommand { get; set; }
        public ICommand RemoveInfSectionCommand { get; set; }

        private void AddInfSectionCommandExecute(object obj)
        {
            _infSectionMenu = new InfSectionAddMenu(_projectDTO);
            _infSectionMenu.ShowDialog();
            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());
        }

        private void UpdateInfSectionCommandExecute(object obj)
        {
            try
            {
                _infSerctionService.UpdateInfSection(SelectedSection);
                Log.Information("Обновление выбранной информационной секции. Название секции - " + SelectedSection.Name);
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при обновлении данных выбранной информационной секций. Смотрите журнал логирования");
                Log.Error("Ошибка при обновлении данных выбранной информаицонной секций - " + ex.Message);
            }
        }

        private void RemoveInfSectionCommandExecute(object obj)
        {
            try
            {
                if (SelectedSection == null)
                {
                    _notifier.ShowWarning("Выберите секцию для удаления");
                    return;
                }
                _infSerctionService.DeleteInfSection(SelectedSection.Id);
                Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());
                _notifier.ShowError("Успешное удаление информационной секции");
                Log.Information("Удаление информационной секции из базы данных. Название информационной секции - " + SelectedSection.Name);
            }
            catch(Exception ex)
            {
                _notifier.ShowError("Ошибка при удалении информационной секции. Смотрите журнал логирования");
                Log.Error("Ошибка при удалении информационной секции - " +  ex.Message);
            }
        }

        public InfSectionVM(IInfSerctionService infSerctionService, IPageService pageService, ProjectDTO projectDTO)
        {
            _infSerctionService = infSerctionService;
            _pageService = pageService;
            _projectDTO = projectDTO;

            Sections = new ObservableCollection<InfSectionDTO>(_infSerctionService.GetInfSections());

            AddInfSectionCommand = new RelayCommand(AddInfSectionCommandExecute);
            UpdateInfSectionCommand = new RelayCommand(UpdateInfSectionCommandExecute);
            RemoveInfSectionCommand = new RelayCommand(RemoveInfSectionCommandExecute);

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
