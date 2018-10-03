using ProjectManager.Data.Models;
using ProjectManager.Data.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Data.Repository
{
    public class TaskRepository
    {
        #region GetParentTask
        /// <summary>
        /// Method to get parent tasks
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetParentTask()
        {
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                var parentTasks = (from task in entity.ParentTasks
                                   select new TaskModel()
                                   {
                                       ParentId = task.Parent_ID,
                                       ParentTask = task.Parent_Task
                                   }).ToList();

                return parentTasks;
            }
        }
        #endregion 

        #region GetAllTask
        /// <summary>
        /// Method to get all task
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetAllTask()
        {
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                var taskList = (from task in entity.Tasks.Include("ParentTask")
                                orderby task.Task_ID descending
                                select new TaskModel()
                                {
                                    TaskId = task.Task_ID,
                                    Task = task.Task1,
                                    ParentTask = task.ParentTask.Parent_Task,
                                    Priority = task.Priority,
                                    StartDate = task.Start_Date,
                                    EndDate = task.End_Date,
                                    ParentId = task.ParentTask.Parent_ID,
                                    IsActive = task.Status
                                }).ToList();

                if (taskList != null)
                {
                    foreach (var item in taskList)
                    {
                        if (item.StartDate != null)
                            item.StartDateString = item.StartDate.ToString();
                        if (item.EndDate != null)
                            item.EndDateString = item.EndDate.ToString();
                    }
                }
                return taskList;
            }
        }
        #endregion

        #region AddorUpdateTask
        /// <summary>
        /// Method to create a new task or update an existing task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public string AddorUpdateTask(TaskModel taskModel)
        {
            string result = string.Empty;
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                if (taskModel != null)
                {
                    Task addTask = new Task();
                    addTask.Task1 = taskModel.Task;
                    if (taskModel.StartDateString != null)
                        addTask.Start_Date = Convert.ToDateTime(taskModel.StartDateString);
                    if (taskModel.EndDateString != null)
                        addTask.End_Date = Convert.ToDateTime(taskModel.EndDateString);
                    addTask.Priority = taskModel.Priority;
                    addTask.Parent_ID = taskModel.ParentId;
                    addTask.Task_ID = taskModel.TaskId;
                    addTask.Status = true;
                    result = addTask.Task_ID == 0 ? "ADD" : "UPDATE";
                    entity.Entry(addTask).State = addTask.Task_ID == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
            }
            return result;
        }
        #endregion

        #region EndTask
        /// <summary>
        /// Method to end task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public bool EndTask(TaskModel taskModel)
        {
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                if (taskModel != null && taskModel.TaskId != 0)
                {
                    Task endTask = new Task();
                    endTask.Task_ID = taskModel.TaskId;
                    endTask.Task1 = taskModel.Task;
                    if (taskModel.StartDateString != null)
                        endTask.Start_Date = Convert.ToDateTime(taskModel.StartDateString);
                    if (taskModel.EndDateString != null)
                        endTask.End_Date = Convert.ToDateTime(taskModel.EndDateString);
                    endTask.Priority = taskModel.Priority;
                    endTask.Parent_ID = taskModel.ParentId;
                    endTask.Status = false;
                    entity.Entry(endTask).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
                return true;
            }

        }
        #endregion
    }
}
