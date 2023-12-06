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
    /// Логика взаимодействия для PageAddMenu.xaml
    /// </summary>
    public partial class PageAddMenu : Window
    {
        IPageService _pageService;

        public PageAddMenu(InfSectionDTO infSectionDTO)
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("ProjectSystemContext"));

            _pageService = kernel.Get<IPageService>();

            DataContext = new PageAddVM(_pageService, infSectionDTO);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
