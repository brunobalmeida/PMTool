using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestPmTool
{
    public class CountryTest
    {

        Country country;
        Boolean recordAccepted = true;
        PmToolDbContext _context = PmToolDbContext.Context;
        string validationResults = "";


        /// <summary>
        /// Initilize a valid object from Province model 
        /// </summary>
        private void Initialize()
        {
            recordAccepted = true;
            validationResults = "";

            country = new Country()
            {
                CountryName = "xUnit testing" + Guid.NewGuid().ToString(),
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
                _context.Entry(country).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }


        [Fact]
        public void ValidCountry_ShouldPass()
        {
            Initialize();
            try
            {
                _context.Country.Add(country);
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
        public void EmptyCountryName_ShouldBeCaught()
        {
            Initialize();
            country.CountryName = "";
            try
            {
                _context.Country.Add(country);
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
        [InlineData("United States")]
        [InlineData("United States   ")]
        [InlineData("    United States")]
        [InlineData(" united states  ")]
        [InlineData("  UnITed STATes  ")]
        public void CountryNameTrimmedCapitalized(string value)
        {

            Initialize();
            country.CountryName= value;

            try
            {
                _context.Country.Add(country);
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
            Assert.Equal("United States", country.CountryName);
        }



    }
}
