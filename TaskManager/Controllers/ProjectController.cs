using System.Collections.Generic;
using System.Web.Http;
using ProjectManager.Business;
using ProjectManager.Data.Models.Custom;

namespace ProjectManager.Controllers
{
    public class ProjectController : ApiController
    {
        ProjectBusiness projectBusiness;

        [HttpGet]
        public List<TaskModel> GetParentTask()
        {
            projectBusiness = new ProjectBusiness();
            var result = projectBusiness.GetParentTask();
            return result;
        }

        [HttpGet]
        public List<TaskModel> GetAllTask()
        {
            projectBusiness = new ProjectBusiness();
            var result = projectBusiness.GetAllTask();
            return result;
        }

        [HttpPost]
        public string InsertTaskDetails(object task)
        {
            string result = string.Empty;
            projectBusiness = new ProjectBusiness();
            result = projectBusiness.InsertTask(task);
            return result;
        }

        [HttpPost]
        public bool UpdateEndTask(object task)
        {
            projectBusiness = new ProjectBusiness();
            return projectBusiness.UpdateTask(task);
        }
     }
}
