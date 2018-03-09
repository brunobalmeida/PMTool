using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class ProjectTest
    {
        Project project;
        Boolean recordAccepted = true;
        PmToolDbContext _context = PmToolDbContext.Context;
        string validationResults = "";

        private void Initialise()
        {
            recordAccepted = true;
            validationResults = "";

            project = new Project()
            {
                ProjectName = "xUnit testing" + Guid.NewGuid().ToString(),
                ProjectOpen = 1,
                StartDate = DateTime.Today,
                EndDate = new DateTime(2500, 2, 2),
                ProjectDescription = "Description test"
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
                _context.Entry(project).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }

        [Fact]
        public void Project_ValidProject_ShouldPass()
        {
            Initialise();
            try
            {
                _context.Project.Add(project);
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
        public void NullName_ShouldBeCaught()
        {
            // Arrange
            Initialise();
            project.ProjectName = null;
            project.ProjectDescription = null;

            //Act
            try
            {
                _context.Project.Add(project);
                _context.EFValidation(); // EF runs all validations except [Remote]
            }
            catch (Exception ex) // a valid Farm object should not go here
            {
                recordAccepted = false;
                validationResults = ex.GetBaseException().Message;
            }
            finally
            {
                Cleanup();  // this removes the Farm object from the EF queue
            }
            Assert.False(recordAccepted, validationResults);
        }

        [Theory]
        [InlineData("New Hamburg")]
        [InlineData("New Hamburg ")]
        [InlineData(" New Hamburg")]
        [InlineData("new hamburg")]
        [InlineData("NEW HAMburg")]
        public void NameTrimmedShiftedLowerCapitalized(string value)
        {
            // arrange
            Initialise();
            project.ProjectName = value;
            // act     
            try
            {
                _context.Project.Add(project);
                _context.EFValidation(); // EF runs all validations except [Remote]
            }
            catch (Exception ex) // a valid Farm object should not go here
            {
                recordAccepted = false;
                validationResults = ex.GetBaseException().Message;
            }
            finally
            {
                Cleanup();  // this removes the Farm object from the EF queue
            }
            // assert
            Assert.True(recordAccepted, validationResults);
            Assert.Equal("New Hamburg", project.ProjectName);
        }
    }
}
