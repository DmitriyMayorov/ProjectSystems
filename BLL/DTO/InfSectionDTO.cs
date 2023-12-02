using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class InfSectionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IDProject { get; set; }

        public InfSectionDTO(InfSection inf) 
        {
            Id = inf.Id;
            Name = inf.Name;
            IDProject = inf.IDProject;
        }

    }
}
