using BLL.DTO;
using DAL.ReportForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILoadFileService
    {
        void SaveStatisticForAllPerson(string filename, List<ReportStatisticByAllPersonDTO> data, string header);
        void SaveStatisitcForCurrentTask(string filename, List<StatisticTrackDTO> data, string header);
        void SaveStatisitcForTasksInCurrentProjectByStates(string filename, List<ReportProjectStatesDTO> data, string header);
    }
}
