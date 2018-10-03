using ProjectManager.Business;
using ProjectManager.Data.Models.Custom;
using System.Collections.Generic;
using System.Web.Http;

namespace ProjectManager.Controllers
{
    public class TaskController : ApiController
    {
        TaskBusiness taskBusiness;

        [Route("Parent")]
        [HttpGet]
        public List<TaskModel> GetParentTask()
        {
            taskBusiness = new TaskBusiness();
            var result = taskBusiness.GetParentTask();
            return result;
        }

        [Route("All")]
        [HttpGet]
        public List<TaskModel> GetAllTask()
        {
            taskBusiness = new TaskBusiness();
            var result = taskBusiness.GetAllTask();
            return result;
        }

        [Route("AddUpdate")]
        [HttpPost]
        public string AddorUpdateTask(object task)
        {
            string result = string.Empty;
            taskBusiness = new TaskBusiness();
            result = taskBusiness.AddorUpdateTask(task);
            return result;
        }

        [Route("End")]
        [HttpPost]
        public bool EndTask(object task)
        {
            taskBusiness = new TaskBusiness();
            return taskBusiness.EndTask(task);
        }
    }
}
