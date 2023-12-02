using BLL.Interfaces;
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
    /// Логика взаимодействия для WorkerMenu.xaml
    /// </summary>
    public partial class WorkerAddMenu : Window
    {
        public WorkerAddMenu(IWorkerService workerService, IPositionService positionService)
        {
            InitializeComponent();

            DataContext = new WorkerAddMenuVM(workerService, positionService);
        }
    }
}
