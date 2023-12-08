using BLL.Interfaces;
using Ninject;
using ProjectSystems.Util.Ninject;
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

using ProjectSystems.ViewModel;

namespace ProjectSystems.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectsMenu.xaml
    /// </summary>
    public partial class ProjectsMenu : UserControl
    { 
        public ProjectsMenu()
        {
            InitializeComponent();
            IProjectService projectService;
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));
            projectService = kernel.Get<IProjectService>();
            DataContext = new ProjectsVM(projectService);
        }

/*        private void TaskGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }*/
    }
}
