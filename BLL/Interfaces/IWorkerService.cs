using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWorkerService
    {
        List<WorkerDTO> GetWorkers();

        WorkerDTO GetWorker(int id);

        void CreateWorker(WorkerDTO track);
        void UpdateWorker(WorkerDTO track);
        void DeleteWorker(int id);
    }
}
