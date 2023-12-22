using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        List<TaskDTO> GetTasks();
        List<TaskDTO> GetTasksByProjectID(int projectID);
        List<TaskDTO> GetTaskByStatusTaskFromCurrentProject(ProjectDTO project, string status);

        TaskDTO GetTask(int id);

        void CreateTask(TaskDTO task);
        void UpdateTask(TaskDTO task);
        void DeleteTask(int id);

        bool ToInProgress(TaskDTO task);
        bool ToReview(TaskDTO task);
        bool ToStage(TaskDTO task);
        bool ToTest(TaskDTO task);
        bool ToReady(TaskDTO task);
    }
}
