using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class OwnersLicenseTest
    {
        OwnersLicense license;
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

            license = new OwnersLicense()
            {
                CompanyName = "xUnit testing" + Guid.NewGuid().ToString(),
                ExpireDate = DateTime.Today,
                Active = "Yes",
                LicenseEmail = "email@email.com"
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
                _context.Entry(license).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }

        [Fact]
        public void ValidOwnersLicense_ShouldPass()
        {
            Initialize();
            try
            {
                _context.OwnersLicense.Add(license);
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
        public void EmptyCompanyName_ShouldBeCaught()
        {
            Initialize();
            license.CompanyName = "";
            try
            {
                _context.OwnersLicense.Add(license);
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
        public void NullName_ShouldBeCaught()
        {
            // Arrange
            Initialize();
            license.CompanyName = null;
            //Act
            try
            {
                _context.OwnersLicense.Add(license);
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
            Assert.False(recordAccepted, validationResults);
        }


        [Theory]
        [InlineData("Smashing Pixels")]
        [InlineData("Smashing Pixels ")]
        [InlineData(" Smashing Pixels")]
        [InlineData("smashing pixels")]
        [InlineData("SmashING PIXEls  ")]
        public void CompanyNameTrimmedCapitalized(string value)
        {

            Initialize();
            license.CompanyName= value;

            try
            {
                _context.OwnersLicense.Add(license);
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
            Assert.Equal("Smashing Pixels", license.CompanyName);
        }

        


    }
}
