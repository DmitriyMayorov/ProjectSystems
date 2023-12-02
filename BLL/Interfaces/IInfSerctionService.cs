using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IInfSerctionService
    {
        List<InfSectionDTO> GetInfSections();

        InfSectionDTO GetInfSection(int id);

        void CreateInfSection(InfSectionDTO infSection);
        void UpdateInfSection(InfSectionDTO infSection);
        void DeleteInfSection(int id);
    }
}
