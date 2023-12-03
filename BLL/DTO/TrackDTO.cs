using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class TrackDTO
    {
        public int Id { get; set; }

        public int IDTask { get; set; }

        public DateTime DateTrack { get; set; }

        public int CountHours { get; set; }

        public int IDWorker { get; set; }

        public string StatusTask { get; set; }

        public TrackDTO() { }

        public TrackDTO(Track tr)
        {
            Id = tr.Id;
            IDTask = tr.IDTask;
            DateTrack = tr.DateTrack;
            CountHours = tr.CountHours;
            IDWorker = tr.IDWorker;
            StatusTask = tr.StatusTask;
        }
    }
}
