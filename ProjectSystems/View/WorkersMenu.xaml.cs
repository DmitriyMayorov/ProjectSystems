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
    /// Логика взаимодействия для WorkersMenu.xaml
    /// </summary>
    public partial class WorkersMenu : UserControl
    {
        IWorkerService workerService;
        IPositionService positionService;

        public WorkersMenu()
        {
            InitializeComponent();

            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            workerService = kernel.Get<IWorkerService>();
            positionService = kernel.Get<IPositionService>();
            DataContext = new WorkersVM(workerService, positionService);
        }
    }
}
