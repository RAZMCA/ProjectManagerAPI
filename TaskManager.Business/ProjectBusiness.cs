﻿using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ProjectManager.Data.Models.Custom;
using ProjectManager.Data.Repository;

namespace ProjectManager.Business
{
    public class ProjectBusiness
    {
        ProjectRepository projectRepository;

        #region GetParentTask
        /// <summary>
        /// Method to fetch the parent task details to load the parent task dropdown
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetParentTask()
        {
            projectRepository = new ProjectRepository();
            var result = projectRepository.GetParentTask();
            return result;
        }
        #endregion

        #region GetAllTask
        /// <summary>
        /// Method to fetch all the task details
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetAllTask()
        {
            projectRepository = new ProjectRepository();
            var result = projectRepository.GetAllTask();
            return result;
        }
        #endregion

        #region InsertTask
        /// <summary>
        /// Method to insert the task details
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public string InsertTask(object taskModel)
        {
            string result = string.Empty;
            projectRepository = new ProjectRepository();
            result = projectRepository.InsertTask(Converter(taskModel));
            return result;
        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Method to end Task 
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public bool UpdateTask(object taskModel)
        {
            projectRepository = new ProjectRepository();
            return projectRepository.UpdateTask(Converter(taskModel));
        }
        #endregion

        #region Converter
        /// <summary>
        /// Method to convert the incoming objects to models
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private TaskModel Converter(object task)
        {
            TaskModel taskModel = new TaskModel();
            string details = task.ToString();
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.DeserializeObject(details);

            if (testModels != null)
            {
                Dictionary<string, object> dic1 = (Dictionary<string, object>)testModels;
                object value;

                if (dic1.TryGetValue("Task", out value))
                    taskModel.Task = value.ToString();
                if (dic1.TryGetValue("ParentId", out value))
                    taskModel.ParentId = string.IsNullOrWhiteSpace(value.ToString()) ? 0 : Convert.ToInt16(value);
                if (dic1.TryGetValue("Priority", out value))
                    taskModel.Priority = string.IsNullOrWhiteSpace(value.ToString()) ? 0 : Convert.ToInt16(value);
                if (dic1.TryGetValue("StartDate", out value))
                    taskModel.StartDateString = value.ToString();
                if (dic1.TryGetValue("EndDate", out value))
                    taskModel.EndDateString = value.ToString();
                if (dic1.TryGetValue("TaskId", out value))
                    taskModel.TaskId = string.IsNullOrWhiteSpace(value.ToString()) ? 0 : Convert.ToInt16(value);
                return taskModel;
            }

            return taskModel;
        }
        #endregion
    }
}
