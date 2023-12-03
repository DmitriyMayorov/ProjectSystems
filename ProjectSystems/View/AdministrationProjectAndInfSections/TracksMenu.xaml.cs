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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization;
using BLL.Interfaces;
using Ninject;
using ProjectSystems.Util.Ninject;
using ProjectSystems.ViewModel.AdministrationProjectAndInfSections;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;

namespace ProjectSystems.View.AdministrationProjectAndInfSections
{
    /// <summary>
    /// Логика взаимодействия для TracksMenu.xaml
    /// </summary>
    public partial class TracksMenu : UserControl
    {
        public TracksMenu()
        {
            InitializeComponent();
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
