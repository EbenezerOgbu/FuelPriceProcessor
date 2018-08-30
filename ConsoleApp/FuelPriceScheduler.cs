using FluentScheduler;
using Processor;
using System;
using System.Configuration;

namespace ConsoleApp
{
    public class FuelPriceScheduler : Registry
    {
        private readonly int _taskExecutionDelay;

        public FuelPriceScheduler()
        {
            _taskExecutionDelay = GetTaskExecutionDelay(); //Note that this has been set to 10 seconds for testing purposes

            RegisterSchedule();
        }

        public void RegisterSchedule()
        {
            Schedule<FuelPriceProcessor>()
                .ToRunNow()
                .AndEvery(_taskExecutionDelay)
                .Seconds();
        }

        private static int GetTaskExecutionDelay()
        {
            var retval = 0;

            var taskExecutionDelay = ConfigurationManager.AppSettings["TaskExecutionDelay"];

            if (!string.IsNullOrWhiteSpace(taskExecutionDelay))
            {
                var tempResult = Convert.ToInt32(taskExecutionDelay);

                if (tempResult >= 0)
                {
                    retval = tempResult;
                }
            }

            return retval;
        }
    }
}
