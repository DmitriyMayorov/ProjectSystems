using BLL.DTO;
using BLL.Interfaces;
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
    /// Логика взаимодействия для TaskAddMenu.xaml
    /// </summary>
    public partial class TaskAddMenu : Window
    {
        public TaskAddMenu(ProjectDTO CurrentProject)
        {
            ITaskService _taskService;
            IWorkerService _workerService;

            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            _taskService = kernel.Get<ITaskService>();
            _workerService = kernel.Get<IWorkerService>();

            DataContext = new TaskAddMenuVM(_taskService, _workerService, CurrentProject);
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
