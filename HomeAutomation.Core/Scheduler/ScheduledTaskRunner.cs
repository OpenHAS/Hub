using HomeAutomation.Logging;
using HomeAutomation.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomation.Core.Scheduler
{
    /// <summary>
    /// Provides a simple way of running scheduled tasks. 
    /// </summary>
    internal class ScheduledTaskRunner : IRunnable
    {
        private bool runloopShouldRun = true;

        private List<ScheduledTaskInfo> taskList;
        private ScheduledTaskInfo[] readOnlyTaskList;

        private Dictionary<ScheduledTaskInfo, Task> runningSystemTasks = new Dictionary<ScheduledTaskInfo, Task>();

        public ScheduledTaskRunner(List<ScheduledTaskInfo> taskList)
        {
            this.taskList = taskList;
            readOnlyTaskList = taskList.ToArray();
        }

        /// <summary>
        /// Stops the run loop.
        /// </summary>
        public void Stop()
        {
            runloopShouldRun = false;
        }

        /// <summary>
        /// Starts the runloop.
        /// </summary>
        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                runloopShouldRun = true;

                while (runloopShouldRun)
                {
                    Step();
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void Step()
        {
            // TODO: implement the use of the ScheduledTimePeriod parameter.

            // make a copy of the main list to ensure it's not being changed while we 
            // are looping
            ScheduledTaskInfo[] localTaskList = readOnlyTaskList;

            foreach (ScheduledTaskInfo taskInfo in localTaskList)
            {
                try
                {
                    // get the system task for this scheduled task.
                    // it may be still running, so we are going to start it again only if it's completed
                    Task systemTask = runningSystemTasks.ContainsKey(taskInfo) ? runningSystemTasks[taskInfo] : null;

                    if (systemTask?.IsCompleted ?? true)
                    {
                        systemTask = Task.Factory.StartNew(() => { taskInfo.Task.TimeElapsed(); });
                        runningSystemTasks[taskInfo] = systemTask;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("[ScheduledTaskRunner] Exception: " + ex.FormatToLog());
                }
            }
        }

        // make sure to create a local copy of the task list each time it changes.
        internal void TaskListChanged()
        {
            readOnlyTaskList = taskList.ToArray();
        }
    }
}