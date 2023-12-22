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
    public class WorkerService : IWorkerService
    {
        private IDbRepos db;
        public WorkerService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<WorkerDTO> GetWorkers()
        {
            return db.Workers.GetList().Select(i => new WorkerDTO(i)).ToList();
        }

        public List<WorkerDTO> GetAnalysts()
        {
            return db.Workers.GetList().Join(db.Positions.GetList(),
                     i => i.IDPosition,
                     j => j.Id,
                     (i, j) => new {
                         Id = i.Id,
                         Pers = i.Person,
                         PS = i.PassportSeries,
                         PN = i.PassportNum,
                         Pos = j.Name,
                         PosID = i.IDPosition
                     })
                    .ToList().Where(i => i.Pos == "Аналитик").ToList()
                    .Select(i => new WorkerDTO() { Id = i.Id, Person = i.Pers, PassportNum = i.PN, PassportSeries = i.PS, IDPosition = i.PosID }).ToList();
        }

        public List<WorkerDTO> GetCoders()
        {
            return db.Workers.GetList().Join(db.Positions.GetList(),
                     i => i.IDPosition,
                     j => j.Id,
                     (i, j) => new {
                         Id = i.Id,
                         Pers = i.Person,
                         PS = i.PassportSeries,
                         PN = i.PassportNum,
                         Pos = j.Name,
                         PosID = i.IDPosition
                     })
                    .ToList().Where(i => i.Pos == "Программист").ToList()
                    .Select(i => new WorkerDTO() { Id = i.Id, Person = i.Pers, PassportNum = i.PN, PassportSeries = i.PS, IDPosition = i.PosID }).ToList();
        }

        public List<WorkerDTO> GetTesters()
        {
            return db.Workers.GetList().Join(db.Positions.GetList(),
                     i => i.IDPosition,
                     j => j.Id,
                     (i, j) => new {
                         Id = i.Id,
                         Pers = i.Person,
                         PS = i.PassportSeries,
                         PN = i.PassportNum,
                         Pos = j.Name,
                         PosID = i.IDPosition
                     })
                    .ToList().Where(i => i.Pos == "Тестировщик").ToList()
                    .Select(i => new WorkerDTO() { Id = i.Id, Person = i.Pers, PassportNum = i.PN, PassportSeries = i.PS, IDPosition = i.PosID }).ToList();
        }

        public WorkerDTO GetWorker(int id)
        {
            return new WorkerDTO(db.Workers.GetItem(id));
        }

        public void CreateWorker(WorkerDTO worker)
        {
            db.Workers.Create(new Worker() { Person = worker.Person, PassportNum = worker.PassportNum,
                                             PassportSeries = worker.PassportSeries, IDPosition = worker.IDPosition});
            SaveChanges();

        }

        public void UpdateWorker(WorkerDTO worker)
        {
            Worker wk = db.Workers.GetItem(worker.Id);
            wk.PassportNum = worker.PassportNum;
            wk.PassportSeries = worker.PassportSeries;
            wk.Person = worker.Person;
            db.Workers.Update(wk);
            SaveChanges();
        }

        public void DeleteWorker(int id)
        {
            db.Workers.Delete(id);
        }
    }
}
