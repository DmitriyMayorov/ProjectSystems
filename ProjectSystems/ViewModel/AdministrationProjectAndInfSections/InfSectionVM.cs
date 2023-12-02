using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class InfSectionVM : ViewModelBase
    {
        IInfSerctionService _infSerctionService;
        IPageService _pageService;

        public InfSectionVM(IInfSerctionService infSerctionService, IPageService pageService)
        {
            _infSerctionService = infSerctionService;
            _pageService = pageService;
        }
    }
}
