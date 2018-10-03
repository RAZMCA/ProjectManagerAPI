using ProjectManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Models.Custom;

namespace ProjectManager.Data.Repository
{
    public class ProjectRepository
    {
        #region GetAllProject
        /// <summary>
        /// Method to get all project list
        /// </summary>
        /// <returns></returns>
        public List<ProjectModel> GetAllProject()
        {
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                var projectList = (from project in entity.Projects
                                   join task in entity.Tasks on project.Project_ID equals task.Project_ID
                                   select new ProjectModel()
                                   {
                                       Project = project.Project1,
                                       Priority = project.Priority,
                                       StartDate = project.Start_Date,
                                       EndDate = project.End_Date,
                                       IsActive = project.Status,
                                       NoOfTasks = project.Tasks.Count(),
                                       CompletedTasks = project.Tasks.Where(x => x.Status == true).Count()
                                   }).ToList();

                return projectList;
            }
        }
        #endregion 

        #region AddOrUpdateProject
        /// <summary>
        /// Method to create new project or update an existing project
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns></returns>
        public string AddOrUpdateProject(ProjectModel projectModel)
        {
            string result = string.Empty;
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                if (projectModel != null)
                {
                    Project addProject = new Project();
                    addProject.Project1 = projectModel.Project;
                    addProject.Start_Date = projectModel.StartDate;
                    addProject.End_Date = projectModel.EndDate;
                    addProject.Priority = projectModel.Priority;
                    addProject.Status = true;
                    result = addProject.Project_ID == 0 ? "ADD" : "UPDATE";
                    entity.Entry(addProject).State = addProject.Project_ID == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
            }
            return result;
        }
        #endregion

        #region SuspendProject
        /// <summary>
        /// Method to suspend project
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns></returns>
        public bool SuspendProject(ProjectModel projectModel)
        {
            using (ProjectManagerEntities entity = new ProjectManagerEntities())
            {
                if (projectModel != null && projectModel.ProjectId != 0)
                {
                    Project suspendProject = new Project();
                    suspendProject.Project1 = projectModel.Project;
                    suspendProject.Project_ID = projectModel.ProjectId;
                    suspendProject.Start_Date = projectModel.StartDate;
                    suspendProject.End_Date = projectModel.EndDate;
                    suspendProject.Priority = projectModel.Priority;
                    suspendProject.Status = false;
                    entity.Entry(suspendProject).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
                return true;
            }

        }
        #endregion
    }
}
