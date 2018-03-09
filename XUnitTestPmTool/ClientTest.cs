using System;
using Xunit;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestPmTool
{
    public class ClientTest
    {
        Client client;
        Boolean recordAccepted = true;
        PmToolDbContext _context = PmToolDbContext.Context;
        string validationResults = "";

        /// <summary>
        /// Initilize a valid object from Client model 
        /// </summary>
        private void Initialize()
        {
            recordAccepted = true;
            validationResults = "";

            client = new Client()
            {
                PersonId = 1,
                BusinessDescription = "TestString",
                WebAddress = "TestString",
                DomainLoginUrl = "TestString",
                DomainUsername = "TestString",
                DomainPassword = "TestString",
                HostingLoginUrl = "TestString",
                HostingUserName = "TestString",
                HostingPassword = "TestString",
                WpLoginUrl = "TestString",
                WpUserName = "TestString",
                WpPassword = "TestString",
                GoogleAnalyticsUrl = "TestString",
                GoogleAnalyticsUsername = "TestString",
                GoogleAnalyticsPassword = "TestString",
                GoogleSearchConsoleUrl = "TestString",
                GoogleSearchConsoleUsername = "TestString",
                GoogleSearchConsolePassword = "TestString",
                BingWemasterToolsUrl = "TestString",
                BingWemasterToolsUsername = "TestString",
                BingWemasterToolsPassword = "TestString",
                GoogleMyBusinessUrl = "TestString",
                GoogleMyBusinessUsername = "TestString",
                GoogleMyBusinessPassword = "TestString",
                KeyWords = "TestString",
                TargetKeyPhases = "TestString",
                TargetAreas = "TestString",
                CompetitorsUrl = "TestString",
                SocialMedia = "TestString",
                SocialMedia2 = "TestString",
                SocialMedia3 = "TestString",
                SocialMedia4 = "TestString",
                OtherMarketingTypes = "TestString",
                MonthlyBudget = 100,
                MonthlyClientTarget = 12,
                ExpandPlaning = 1,
                MarketingGoals = "TestString",

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
                _context.Entry(client).State = EntityState.Detached;
            }
            catch (Exception)
            { }
        }

        [Fact]
        public void ValidClient_ShouldPass()
        {
            Initialize();
            try
            {
                _context.Client.Add(client);
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
        public void EmptyBusinessDescription_ShouldBeCaught()
        {
            Initialize();
            client.BusinessDescription = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyWebAddress_ShouldBeCaught()
        {
            Initialize();
            client.WebAddress = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyDomainLoginURL_ShouldBeCaught()
        {
            Initialize();
            client.DomainLoginUrl = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyDomainUserName_ShouldBeCaught()
        {
            Initialize();
            client.DomainUsername = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyDomainPassword_ShouldBeCaught()
        {
            Initialize();
            client.DomainPassword = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyHostingURL_ShouldBeCaught()
        {
            Initialize();
            client.HostingLoginUrl = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyHostingUserName_ShouldBeCaught()
        {
            Initialize();
            client.HostingUserName = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyHostingPassword_ShouldBeCaught()
        {
            Initialize();
            client.HostingPassword = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyTargetKeyPhrases_ShouldBeCaught()
        {
            Initialize();
            client.TargetKeyPhases = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyTargetAreas_ShouldBeCaught()
        {
            Initialize();
            client.TargetAreas = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyCompetitorsUrl_ShouldBeCaught()
        {
            Initialize();
            client.CompetitorsUrl = "";
            try
            {
                _context.Client.Add(client);
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
        public void EmptyMonthlyBudget_ShouldBeCaught()
        {
            Initialize();
            string temp = "";
            
            try
            {
                client.MonthlyBudget = int.Parse(temp);
                _context.Client.Add(client);
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
        public void EmptyMonthlyClientTarget_ShouldBeCaught()
        {
            Initialize();
            string temp = "";
            try
            {
                client.MonthlyClientTarget = int.Parse(temp);
                _context.Client.Add(client);
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
