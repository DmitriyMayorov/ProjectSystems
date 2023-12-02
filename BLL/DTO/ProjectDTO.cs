using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DeadLine { get; set; }

        public ProjectDTO() { }

        public ProjectDTO(Project pr)
        {
            Id = pr.Id;
            Name = pr.Name;
            DeadLine = pr.DeadLine;
        }
    }
}
