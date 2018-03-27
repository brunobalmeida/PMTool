using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class TaskListTest
    {
        TaskList taskList;
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

            taskList = new TaskList()
            {
                TaskListName = "xUnit testing" + Guid.NewGuid().ToString(),
                ProjectId = 1,
                TaskListOpen = 1
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
                _context.Entry(taskList).State = EntityState.Detached;
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
                _context.TaskList.Add(taskList);
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
        public void EmptyTaskListName_ShouldBeCaught()
        {
            Initialize();
            taskList.TaskListName = "";
            try
            {
                _context.TaskList.Add(taskList);
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
        [InlineData("Task List")]
        [InlineData("Task List ")]
        [InlineData(" Task List ")]
        [InlineData(" Task List")]
        [InlineData("task list")]
        [InlineData(" Task list")]
        [InlineData("  tAsk liST ")]
        [InlineData("  tAsk LiSt ")]
        [InlineData("tAsk lIST ")]
        [InlineData("TASK LIST")]
        public void TasknameTrimmedCapitalized(string value)
        {

            Initialize();
            taskList.TaskListName= value;

            try
            {
                _context.TaskList.Add(taskList);
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
            Assert.Equal("Task List", taskList.TaskListName);
        }

    }
}
