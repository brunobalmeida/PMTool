using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Client
    {
        public Client()
        {
            Project = new HashSet<Project>();
        }

        public int ClientId { get; set; }
        public int PersonId { get; set; }
        public string BusinessDescription { get; set; }
        public string WebAddress { get; set; }
        public string DomainLoginUrl { get; set; }
        public string DomainUsername { get; set; }
        public string DomainPassword { get; set; }
        public string HostingLoginUrl { get; set; }
        public string HostingUserName { get; set; }
        public string HostingPassword { get; set; }
        public string WpLoginUrl { get; set; }
        public string WpUserName { get; set; }
        public string WpPassword { get; set; }
        public string GoogleAnalyticsUrl { get; set; }
        public string GoogleAnalyticsUsername { get; set; }
        public string GoogleAnalyticsPassword { get; set; }
        public string GoogleSearchConsoleUrl { get; set; }
        public string GoogleSearchConsoleUsername { get; set; }
        public string GoogleSearchConsolePassword { get; set; }
        public string BingWemasterToolsUrl { get; set; }
        public string BingWemasterToolsUsername { get; set; }
        public string BingWemasterToolsPassword { get; set; }
        public string GoogleMyBusinessUrl { get; set; }
        public string GoogleMyBusinessUsername { get; set; }
        public string GoogleMyBusinessPassword { get; set; }
        public string KeyWords { get; set; }
        public string TargetKeyPhases { get; set; }
        public string TargetAreas { get; set; }
        public string CompetitorsUrl { get; set; }
        public string SocialMedia { get; set; }
        public string SocialMedia2 { get; set; }
        public string SocialMedia3 { get; set; }
        public string SocialMedia4 { get; set; }
        public string OtherMarketingTypes { get; set; }
        public int MonthlyBudget { get; set; }
        public int? MonthlyClientTarget { get; set; }
        public int? ExpandPlaning { get; set; }
        public string MarketingGoals { get; set; }

        public Person Person { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
