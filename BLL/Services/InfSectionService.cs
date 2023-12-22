using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InfSectionService : IInfSerctionService
    {
        private IDbRepos db;
        public InfSectionService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<InfSectionDTO> GetInfSections()
        {
            return db.InfSections.GetList().Select(i => new InfSectionDTO(i)).ToList();
        }

        public List<InfSectionDTO> GetInfSectionsForCurrentProject(ProjectDTO project)
        {
            return db.InfSections.GetList().Where(i => i.IDProject == project.Id).Select(i => new InfSectionDTO(i)).ToList();
        }

        public InfSectionDTO GetInfSection(int id)
        {
            return new InfSectionDTO(db.InfSections.GetItem(id));
        }

        public void CreateInfSection(InfSectionDTO infSection)
        {
            db.InfSections.Create(new InfSection()
            {
/*                Id = infSection.Id,*/
                Name = infSection.Name,
                IDProject = infSection.IDProject
            });
            SaveChanges();
        }

        public void UpdateInfSection(InfSectionDTO infSection)
        {
            InfSection inf = db.InfSections.GetItem(infSection.Id);
            inf.Name = infSection.Name;
            inf.IDProject = infSection.IDProject;
            SaveChanges();
        }

        public void DeleteInfSection(int id)
        {
            db.InfSections.Delete(id);
        }
    }
}
