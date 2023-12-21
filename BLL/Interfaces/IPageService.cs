using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPageService
    {
        List<PageDTO> GetPages();
        List<PageDTO> GetPagesForCurrentInfSection(InfSectionDTO infSectionDTO);

        PageDTO GetPage(int id);

        void CreatePage(PageDTO page);
        void UpdatePage(PageDTO page);
        void DeletePage(int id);
    }
}
