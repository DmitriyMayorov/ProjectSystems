using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BLL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }

        public string Person { get; set; }

        public long PassportNum { get; set; }

        public long PassportSeries { get; set; }

        public int IDPosition { get; set; }

        public PositionDTO Position { get; set; }

        public WorkerDTO() { }

        public WorkerDTO(Worker wk)
        {
            Id = wk.Id;
            Person = wk.Person;
            PassportNum = wk.PassportNum;
            PassportSeries = wk.PassportSeries;
            IDPosition = wk.IDPosition;
            Position = new PositionDTO(wk.Position);
        }
    }
}
