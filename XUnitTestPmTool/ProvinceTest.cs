using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestPmTool
{
    public class ProvinceTest
    {

        Province province;
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

            province = new Province()
            {
                ProvinceName = "xUnit testing" + Guid.NewGuid().ToString(),
                ProvinceCode = "TT",
                CountryId = 1
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
                _context.Entry(province).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }


        [Fact]
        public void ValidProvince_ShouldPass()
        {
            Initialize();
            try
            {
                _context.Province.Add(province);
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
        public void EmptyProvinceName_ShouldBeCaught()
        {
            Initialize();
            province.ProvinceName = "";
            try
            {
                _context.Province.Add(province);
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
        public void EmptyProvinceCode_ShouldBeCaught()
        {
            Initialize();
            province.ProvinceCode = "";
            try
            {
                _context.Province.Add(province);
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
        public void ProvinceCodeGreaterThan2Characters_ShouldBeCaught()
        {
            Initialize();
            province.ProvinceCode = "BBB";
            try
            {
                _context.Province.Add(province);
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
        [InlineData("British Columbia")]
        [InlineData("   british columbia ")]
        [InlineData("   british COLumbia")]
        [InlineData("British ColumBia    ")]
        [InlineData("      BRitish Columbia  ")]
        public void ProvinceNameTrimmedCapitalized(string value)
        {

            Initialize();
            province.ProvinceName= value;

            try
            {
                _context.Province.Add(province);
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
            Assert.Equal("British Columbia", province.ProvinceName);
        }

        [Theory]
        [InlineData("BC")]
        [InlineData("bc")]
        [InlineData("bC")]
        [InlineData(" Bc")]
        [InlineData("bc ")]
        public void ProvinceCodeTrimmedUpper(string value)
        {

            Initialize();
            province.ProvinceCode = value;

            try
            {
                _context.Province.Add(province);
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
            Assert.Equal("BC", province.ProvinceCode);
        }


    }
}
