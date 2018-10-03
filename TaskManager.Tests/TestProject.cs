using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Controllers;
using ProjectManager.Data.Models.Custom;
using System;
using System.Web.Script.Serialization;

namespace ProjectManager.Tests
{
    [TestClass]
    public class TestProject
    {
        TaskController taskController = new TaskController();

        [TestMethod]
        public void GetAllTask()
        {
            var result = taskController.GetAllTask();
            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetParentTask()
        {
            var result = taskController.GetParentTask();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void InsertTask()
        {
            TaskModel addTask = new TaskModel();
            addTask.Task = "Task New";
            addTask.StartDate = DateTime.Now;
            addTask.EndDate = DateTime.Now;
            addTask.Priority = 15;
            addTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(addTask);
            var isAdded = taskController.AddorUpdateTask(testModels);
            Assert.AreEqual("ADD", isAdded);
        }

        [TestMethod]
        public void UpdateTask()
        {
            TaskModel updateTask = new TaskModel();
            updateTask.TaskId = 2005;
            updateTask.Task = "Task from Test";
            updateTask.StartDate = DateTime.Now;
            updateTask.EndDate = DateTime.Now;
            updateTask.Priority = 30;
            updateTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(updateTask);
            var isUpdated = taskController.AddorUpdateTask(testModels);
            Assert.AreEqual("UPDATE", isUpdated);
        }

        [TestMethod]
        public void EndTask()
        {
            TaskModel endTask = new TaskModel();
            endTask.TaskId = 2005;
            endTask.Task = "Task from Test";
            endTask.StartDate = DateTime.Now;
            endTask.EndDate = DateTime.Now;
            endTask.Priority = 30;
            endTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(endTask);
            var isSuccess = taskController.EndTask(testModels);
            Assert.AreEqual(true, isSuccess);
        }
    }
}
