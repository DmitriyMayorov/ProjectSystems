using BLL.Interfaces;
using Ninject;
using ProjectSystems.Util.Ninject;
using ProjectSystems.ViewModel;
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

namespace ProjectSystems.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectAddMenu.xaml
    /// </summary>
    public partial class ProjectAddMenu : Window
    {
        IProjectService _projectService;

        public ProjectAddMenu()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            _projectService = kernel.Get<IProjectService>();
            DataContext = new ProjectAddMenuVM(_projectService);
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
