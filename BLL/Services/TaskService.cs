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
    public class TaskService : ITaskService
    {
        private IDbRepos db;
        public TaskService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<TaskDTO> GetTasks()
        {
            return db.Tasks.GetList().Select(i => new TaskDTO(i)).ToList();
        }
        
        public List<TaskDTO> GetTasksByProjectID(int projectID)
        {
            return db.Tasks.GetList().Select(i => new TaskDTO(i)).Where(i => i.IDProject ==  projectID).ToList();
        }

        public List<TaskDTO> GetTaskByStatusTaskFromCurrentProject(ProjectDTO project, string status)
        {
            if (status == "Analyst")
                return db.Tasks.GetList().Select(i => new TaskDTO(i)).Where(i => i.IDProject == project.Id).ToList();
            else if (status == "Coder")
                return db.Tasks.GetList().Select(i => new TaskDTO(i)).
                                Where(i => i.IDProject == project.Id && (i.State == "InProgress" || i.State == "Review" || i.State == "Stage")).ToList();
            else if (status == "Tester")
                return db.Tasks.GetList().Select(i => new TaskDTO(i)).Where(i => i.IDProject == project.Id && (i.State == "Test")).ToList();
            else
                return new List<TaskDTO>();
        }

        public TaskDTO GetTask(int id)
        {
            return new TaskDTO(db.Tasks.GetItem(id));
        }

        public void CreateTask(TaskDTO task)
        {
            db.Tasks.Create(new DAL.EF.Task()
            {
/*                Id = task.Id,*/
                Name = task.Name,
                Description = task.Description,
                State = task.State,
                Category = task.Category,
                IDProject = task.IDProject,
                IDWorkerAnalyst = task.IDWorkerAnalyst,
                IDWorkerCoder = task.IDWorkerCoder,
                IDWorkerMentor = task.IDWorkerMentor,
                IDWorkerTester = task.IDWorkerTester,
                Deadline = task.Deadline,
                Priority = task.Priority,
            });
            SaveChanges();
        }

        public void UpdateTask(TaskDTO taskdto)
        {
            DAL.EF.Task task = db.Tasks.GetItem(taskdto.Id);
            task.Name = taskdto.Name;
            task.Description = taskdto.Description;
            task.IDProject = taskdto.IDProject;
            task.State = taskdto.State;
            task.Category = taskdto.Category;
            task.IDWorkerAnalyst = taskdto.IDWorkerAnalyst;
            task.IDWorkerCoder = taskdto.IDWorkerCoder;
            task.IDWorkerMentor = taskdto.IDWorkerMentor;
            task.IDWorkerTester = taskdto.IDWorkerTester;
            task.Deadline = taskdto.Deadline;
            task.Priority = taskdto.Priority;
            SaveChanges();
        }

        public void DeleteTask(int id)
        {
            db.Tasks.Delete(id);
        }

        public bool ToInProgress(TaskDTO task)
        {
            DAL.EF.Task taskCurrent = db.Tasks.GetItem(task.Id);
            if (taskCurrent.State == "Ready")
                return false;
            taskCurrent.State = "InProgress";
            SaveChanges();
            return true;
        }

        public bool ToReview(TaskDTO task)
        {
            DAL.EF.Task taskCurrent = db.Tasks.GetItem(task.Id);
            if (taskCurrent.State != "InProgress")
                return false;
            taskCurrent.State = "Review";
            SaveChanges();
            return true;
        }

        public bool ToStage(TaskDTO task)
        {
            DAL.EF.Task taskCurrent = db.Tasks.GetItem(task.Id);
            if (taskCurrent.State != "Review")
                return false;
            taskCurrent.State = "Stage";
            SaveChanges();
            return true;
        }

        public bool ToTest(TaskDTO task)
        {
            DAL.EF.Task taskCurrent = db.Tasks.GetItem(task.Id);
            if (taskCurrent.State != "Stage")
                return false;
            taskCurrent.State = "Test";
            SaveChanges();
            return true;
        }

        public bool ToReady(TaskDTO task)
        {
            DAL.EF.Task taskCurrent = db.Tasks.GetItem(task.Id);
            if (taskCurrent.State != "Test")
                return false;
            taskCurrent.State = "Ready";
            SaveChanges();
            return true;
        }
    }
}
