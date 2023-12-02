using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class PositionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PositionDTO() { }

        public PositionDTO(Position pos)
        {
            Id = pos.Id;
            Name = pos.Name;
        }
    }
}
