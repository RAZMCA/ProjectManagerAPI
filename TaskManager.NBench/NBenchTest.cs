using NBench;
using ProjectManager.Controllers;
using ProjectManager.Data.Models.Custom;
using System;
using System.Web.Script.Serialization;

namespace ProjectManager.NBench
{
    public class NBenchTest
    {
        private Counter _objCounter;

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            _objCounter = context.GetCounter("ProjectCounter");
        }

        #region TASK
        TaskController taskController;

        [PerfBenchmark(Description = "Counter iteration performance test GETPARENTTASK()", NumberOfIterations = 3, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_GetParentTask()
        {
            var bytes = new byte[1024];
            taskController = new TaskController();
            var result = taskController.GetParentTask();
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test for GETALLTASK()", NumberOfIterations = 3, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_GetAllTask()
        {
            var bytes = new byte[1024];
            taskController = new TaskController();
            var result = taskController.GetAllTask();
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test ADDTASK()", NumberOfIterations = 3, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_AddTask()
        {
            var bytes = new byte[1024];
            taskController = new TaskController();
            TaskModel addTask = new TaskModel();
            addTask.Task = "Task from NBench Addtask()";
            addTask.StartDate = DateTime.Now;
            addTask.EndDate = DateTime.Now;
            addTask.Priority = 15;
            addTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(addTask);
            var isAdded = taskController.AddorUpdateTask(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test UPDATETASK()", NumberOfIterations = 3, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_UpdateTask()
        {
            var bytes = new byte[1024];
            taskController = new TaskController();
            TaskModel updateTask = new TaskModel();
            updateTask.TaskId = 2005;
            updateTask.Task = "Task from NBench UpdateTask()";
            updateTask.StartDate = DateTime.Now;
            updateTask.EndDate = DateTime.Now;
            updateTask.Priority = 30;
            updateTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(updateTask);
            var isUpdated = taskController.AddorUpdateTask(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test ENDTASK()", NumberOfIterations = 3, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_EndTask()
        {
            var bytes = new byte[1024];
            taskController = new TaskController();
            TaskModel endTask = new TaskModel();
            endTask.TaskId = 2005;
            endTask.Task = "Task from NBench EndTask()";
            endTask.StartDate = DateTime.Now;
            endTask.EndDate = DateTime.Now;
            endTask.Priority = 30;
            endTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(endTask);
            var isSuccess = taskController.EndTask(testModels);
            _objCounter.Increment();
        }
        #endregion

        #region PROJECT

        #endregion

        #region USER

        #endregion

        [PerfCleanup]
        public void Clean()
        {
            //does nothing
        }
    }
}
