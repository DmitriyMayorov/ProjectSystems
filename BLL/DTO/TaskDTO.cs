using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DAL.EF;

namespace BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? IDWorkerCoder { get; set; }

        public int? IDWorkerAnalyst { get; set; }

        public int? IDWorkerMentor { get; set; }

        public int? IDWorkerTester { get; set; }

        public int IDProject { get; set; }

        public string Category { get; set; }

        public string State { get; set; }

        public string Priority { get; set; }

        public int? IDWorkerCreater { get; set; }

        public DateTime? Deadline { get; set; }

        public WorkerDTO WorkerAnalyst { get; set; }

        public WorkerDTO WorkerCoder { get; set; }

        public WorkerDTO WorkerTechlid { get; set; }

        public WorkerDTO WorkerTester { get; set; }

        public WorkerDTO WorkerCreater { get; set; }

        public ProjectDTO Project { get; set; }

        public TaskDTO() { }

        public TaskDTO(Task tk)
        {
            Id = tk.Id;
            Name = tk.Name;
            Description = tk.Description;
            IDWorkerCoder = tk.IDWorkerCoder;
            IDWorkerAnalyst = tk.IDWorkerAnalyst;
            IDWorkerMentor = tk.IDWorkerMentor;
            IDWorkerTester = tk.IDWorkerTester;
            IDProject = tk.IDProject;
            Category = tk.Category;
            State = tk.State;
            Priority = tk.Priority;
            IDWorkerCreater = tk.IDWorkerCreater;
            Deadline = tk.Deadline;

            WorkerAnalyst = new WorkerDTO(tk.Worker);
            WorkerCoder = new WorkerDTO(tk.Worker1);
/*            WorkerTechlid = new WorkerDTO(tk.Worker2);*/
            WorkerTester = new WorkerDTO(tk.Worker3);
            WorkerCreater = new WorkerDTO(tk.Worker4);

            Project = new ProjectDTO(tk.Project);
        }
    }
}
