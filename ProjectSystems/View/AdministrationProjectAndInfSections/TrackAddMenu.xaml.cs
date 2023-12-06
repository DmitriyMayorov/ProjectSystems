using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Ninject;
using ProjectSystems.Util.Ninject;
using ProjectSystems.ViewModel.AdministrationProjectAndInfSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectSystems.View.AdministrationProjectAndInfSections
{
    /// <summary>
    /// Логика взаимодействия для TrackAddMenu.xaml
    /// </summary>
    public partial class TrackAddMenu : Window
    {
        ITrackService trackService;

        public TrackAddMenu(TaskDTO taskDTO)
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            trackService = kernel.Get<ITrackService>();

            DataContext = new TrackAddVM(trackService, taskDTO);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
