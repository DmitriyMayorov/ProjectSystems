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
    public class PageDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TextSection { get; set; }

        public int IDSection { get; set; }

        public PageDTO() { }

        public PageDTO(Page pg)
        {
            Id = pg.Id;
            Name = pg.Name;
            TextSection = pg.TextSection;
            IDSection = pg.IDSection;
        }
    }
}
