using BLL.DTO;
using BLL.Interfaces;
using ProjectSystems.Util.TemplateElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSystems.ViewModel.AdministrationProjectAndInfSections
{
    public class PagesVM : ViewModelBase
    {
        InfSectionDTO _currentInfSection;

        public PagesVM(InfSectionDTO currentInfSection) 
        {
            _currentInfSection = currentInfSection;
        }
    }
}
