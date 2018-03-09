﻿using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class PersonTest
    {        
        Person person;
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

            person = new Person()
            {
                FirstName = "John",
                LastName = "Wick",
                MiddleName = "S",
                Address = "111 X St",
                Email = "a@a.com",
                OwnersLicenseId = 2,
                Address2 = "222 X St",
                ProvinceId = 1,
                PostalCode = "N2N2N2",
                PhoneNumber = "1111111111"
                
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
                _context.Entry(person).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        public void FirstNameValidations_ShouldNotPass(string value)
        {
            Initialize();
            person.FirstName = value;
            try
            {
                _context.Person.Add(person);
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
        [InlineData(" ")]
        [InlineData("")]
        public void LastNameValidations_ShouldNotPass(string value)
        {
            Initialize();
            person.LastName = value;
            try
            {
                _context.Person.Add(person);
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
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("@")]
        [InlineData("a")]
        [InlineData(".")]
        [InlineData("@.")]
        public void EmaileValidations_ShouldNotPass(string value)
        {
            Initialize();
            person.Email = value;
            try
            {
                _context.Person.Add(person);
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
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(".")]
        public void OwnerLicenseIdValidations_ShouldNotPass(string value)
        {
            Initialize();
            
            string tempOwnersLicenseId = value;
            try
            {
                person.OwnersLicenseId = Convert.ToInt32(value);
                _context.Person.Add(person);
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
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(".")]
        public void ProvinceIdValidations_ShouldNotPass(string value)
        {
            Initialize();

            string tempOwnersLicenseId = value;
            try
            {
                person.OwnersLicenseId = Convert.ToInt32(value);
                _context.Person.Add(person);
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
