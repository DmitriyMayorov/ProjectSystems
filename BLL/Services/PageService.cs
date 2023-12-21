using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PageService : IPageService
    {
        private IDbRepos db;
        public PageService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<PageDTO> GetPages()
        {
            return db.Pages.GetList().Select(i => new PageDTO(i)).ToList();
        }

        public List<PageDTO> GetPagesForCurrentInfSection(InfSectionDTO infSectionDTO)
        {
            return db.Pages.GetList().Select(i => new PageDTO(i)).Where(i => i.IDSection == infSectionDTO.Id).ToList();
        }

        public PageDTO GetPage(int id)
        {
            return new PageDTO(db.Pages.GetItem(id));
        }

        public void CreatePage(PageDTO page)
        {
            db.Pages.Create(new Page() { /*Id = page.Id, */ Name = page.Name, TextSection = page.TextSection,
                                         IDSection = page.IDSection});
            SaveChanges();
        }

        public void UpdatePage(PageDTO page)
        {
            Page pg = db.Pages.GetItem(page.Id);
            pg.Name = page.Name;
            pg.TextSection = page.TextSection;
            pg.IDSection = page.IDSection;
            SaveChanges();
        }

        public void DeletePage(int id)
        {
            db.Pages.Delete(id);
        }

    }
}
