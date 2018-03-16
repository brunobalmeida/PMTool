using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class TaskInfoTest
    {
        TaskInfo taskInfo;
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

            taskInfo = new TaskInfo()
            {
                TaskId = 1,
                TaskNote = "xUnit testing" + Guid.NewGuid().ToString()
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
                _context.Entry(taskInfo).State = EntityState.Detached;
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
                _context.TaskInfo.Add(taskInfo);
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
        public void EmptyTaskNote_ShouldBeCaught()
        {
            Initialize();
            taskInfo.TaskNote = "";
            try
            {
                _context.TaskInfo.Add(taskInfo);
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
        public void TaskNoteLengthGreaterThan255_ShouldBeCaught()
        {
            Initialize();
            taskInfo.TaskNote = "CharactersCharactersCharactersCharacters" +
                "CharactersCharactersCharactersCharactersCharactersCharacters" +
                "CharactersCharactersCharactersCharactersCharactersCharactersCharacters" +
                "CharactersCharactersCharactersCharactersCharactersCharactersCharactersCharactersCharacters";
            try
            {
                _context.TaskInfo.Add(taskInfo);
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
