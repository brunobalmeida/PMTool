using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class TaskTest
    {
        Task task;
        Boolean recordAccepted = true;
        PmToolDbContext _context = PmToolDbContext.Context;
        string validationResults = "";

        /// <summary>
        /// Initilize a valid object from OwnersLicense model 
        /// </summary>
        private void Initialize()
        {
            recordAccepted = true;
            validationResults = "";

            task = new Task()
            {
                EmployeeId = 1,
                TaskName = "xUnit testing" + Guid.NewGuid().ToString(),
                TaskListId = 1,
                TaskWeight = 1,
                TaskDescription = "xUnit testing" + Guid.NewGuid().ToString(),
                ExpectedDate = DateTime.Now.AddDays(1),
                TaskDuration = 1,
                TaskActiveFlag = 1
            };
        }

        /// <summary>
        /// After testing an insert or update to the database, this can be called to clear
        /// an object that failed and became stuck in the EF queue, so it does not
        /// affect subsequent tests.
        /// </summary>
        private void Cleanup()
        {
            try
            {
                _context.Entry(task).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }

        [Fact]
        public void ValidTask_ShouldPass()
        {
            Initialize();
            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception e)
            {
                recordAccepted = false;
                validationResults = e.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }

            Assert.True(recordAccepted, validationResults);
        }

        [Fact]
        public void EmptyTaskName_ShouldBeCaught()
        {
            Initialize();
            task.TaskName = "";
            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception e)
            {
                recordAccepted = false;
                validationResults = e.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }

            Assert.False(recordAccepted, validationResults);
        }

        [Fact]
        public void EmptyTaskDecription_ShouldBeCaught()
        {
            Initialize();
            task.TaskDescription = "";
            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception e)
            {
                recordAccepted = false;
                validationResults = e.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }

            Assert.False(recordAccepted, validationResults);
        }

        [Theory]
        [InlineData("Task Name")]
        [InlineData("Task Name ")]
        [InlineData(" Task Name")]
        [InlineData(" Task Name ")]
        [InlineData("task name")]
        [InlineData(" Task name")]
        [InlineData("  tAsk naMe ")]
        [InlineData("  tAsk nAMe ")]
        [InlineData("tAsk nAMe ")]
        [InlineData("TASK NAME")]
        public void TasknameTrimmedCapitalized(string value)
        {

            Initialize();
            task.TaskName= value;

            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception ex)
            {
                recordAccepted = false;
                validationResults = ex.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }
            Assert.True(recordAccepted, validationResults);
            Assert.Equal("Task Name", task.TaskName);
        }


        [Fact]
        public void TaskDurationLessThanZero_ShouldBeCaught()
        {
            Initialize();
            task.TaskDuration = -1;
            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception e)
            {
                recordAccepted = false;
                validationResults = e.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }

            Assert.False(recordAccepted, validationResults);
        }

        [Fact]
        public void  DateTimeInThePast_ShouldBeCaught()
        {
            Initialize();
            task.ExpectedDate = DateTime.Now.AddDays(-1);
            try
            {
                _context.Task.Add(task);
                _context.EFValidation();
            }
            catch (Exception e)
            {
                recordAccepted = false;
                validationResults = e.GetBaseException().Message;
            }
            finally
            {
                Cleanup();
            }

            Assert.False(recordAccepted, validationResults);
        }

    }
}
