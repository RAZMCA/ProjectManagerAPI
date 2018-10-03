using NBench;
using ProjectManager.Controllers;
using ProjectManager.Data.Models.Custom;
using System;
using System.Web.Script.Serialization;

namespace ProjectManager.NBench
{
    public class NBenchTest
    {
        TaskController taskController = new TaskController();
        private Counter _objCounter;

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            _objCounter = context.GetCounter("ProjectCounter");
        }

        //[PerfBenchmark(Description = "Counter iteration performance test GETPARENTTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        //[CounterMeasurement("ProjectCounter")]
        //[MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        //public void NBench_GetParentTask()
        //{
        //    var bytes = new byte[1024];
        //    var result = controller.GetParentTask();
        //    _objCounter.Increment();
        //}

        //[PerfBenchmark(Description = "Counter iteration performance test for GETALLTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        //[CounterMeasurement("ProjectCounter")]
        //[MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        //public void NBench_GetAllTask()
        //{
        //    var bytes = new byte[1024];
        //    var result = controller.GetParentTask();
        //    _objCounter.Increment();
        //}

        [PerfBenchmark(Description = "Counter iteration performance test INSERTTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_InsertTask()
        {
            var bytes = new byte[1024];
            TaskModel addTask = new TaskModel();
            addTask.Task = "Task NBench";
            addTask.StartDate = DateTime.Now;
            addTask.EndDate = DateTime.Now;
            addTask.Priority = 15;
            addTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(addTask);
            var isAdded = taskController.AddorUpdateTask(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test UPDATETASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_UpdateTask()
        {
            var bytes = new byte[1024];
            TaskModel updateTask = new TaskModel();
            updateTask.TaskId = 2005;
            updateTask.Task = "Task from NBench";
            updateTask.StartDate = DateTime.Now;
            updateTask.EndDate = DateTime.Now;
            updateTask.Priority = 30;
            updateTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(updateTask);
            var isUpdated = taskController.AddorUpdateTask(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test ENDTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_EndTask()
        {
            var bytes = new byte[1024];
            TaskModel endTask = new TaskModel();
            endTask.TaskId = 2005;
            endTask.Task = "Task from NBench";
            endTask.StartDate = DateTime.Now;
            endTask.EndDate = DateTime.Now;
            endTask.Priority = 30;
            endTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(endTask);
            var isSuccess = taskController.EndTask(testModels);
            _objCounter.Increment();
        }

        [PerfCleanup]
        public void Clean()
        {
            //does nothing
        }
    }
}
