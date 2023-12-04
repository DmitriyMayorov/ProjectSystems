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
    /// Логика взаимодействия для InfSectionAddMenu.xaml
    /// </summary>
    public partial class InfSectionAddMenu : Window
    {
        IInfSerctionService _infSerctionService;

        public InfSectionAddMenu(ProjectDTO projectDTO)
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            _infSerctionService = kernel.Get<IInfSerctionService>();

            DataContext = new InfSectionAddVM(_infSerctionService, projectDTO);
        }
    }
}
