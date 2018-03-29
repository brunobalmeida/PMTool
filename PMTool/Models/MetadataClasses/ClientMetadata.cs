using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models
{
    [ModelMetadataType(typeof(ClientMetadata))]
    public partial class Client : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(BusinessDescription))
            {
                yield return new ValidationResult("Business Description is Required",
                      new[] { nameof(BusinessDescription) });
            }
            if (String.IsNullOrEmpty(WebAddress))
            {
                yield return new ValidationResult("WebAddress is Required",
                      new[] { nameof(WebAddress) });
            }
            if (String.IsNullOrEmpty(DomainLoginUrl))
            {
                yield return new ValidationResult("Domain Login Url is Required",
                      new[] { nameof(DomainLoginUrl) });
            }
            if (String.IsNullOrEmpty(DomainUsername))
            {
                yield return new ValidationResult("Domain Username is Required",
                      new[] { nameof(DomainUsername) });
            }
            if (String.IsNullOrEmpty(DomainPassword))
            {
                yield return new ValidationResult("Domain Password is Required",
                      new[] { nameof(DomainPassword) });
            }
            if (String.IsNullOrEmpty(HostingLoginUrl))
            {
                yield return new ValidationResult("Hosting Url is Required",
                      new[] { nameof(HostingLoginUrl) });
            }

            if (String.IsNullOrEmpty(HostingUserName))
            {
                yield return new ValidationResult("Hosting Username is Required",
                      new[] { nameof(HostingUserName) });
            }
            if (String.IsNullOrEmpty(HostingPassword))
            {
                yield return new ValidationResult("Hosting Password is Required",
                      new[] { nameof(HostingPassword) });
            }

            if (String.IsNullOrEmpty(TargetKeyPhases))
            {
                yield return new ValidationResult("Target Key Phrases are Required",
                      new[] { nameof(TargetKeyPhases) });
            }

            if (String.IsNullOrEmpty(TargetAreas))
            {
                yield return new ValidationResult("Target Areas are Required",
                      new[] { nameof(TargetAreas) });
            }

            if (String.IsNullOrEmpty(CompetitorsUrl))
            {
                yield return new ValidationResult("Competitor URLs Are Required",
                      new[] { nameof(CompetitorsUrl) });
            }

            if (String.IsNullOrEmpty(MonthlyBudget.ToString()))
            {
                yield return new ValidationResult("Monthly Budget Number is Required",
                      new[] { nameof(MonthlyBudget) });
            }

            if (String.IsNullOrEmpty(MonthlyClientTarget.ToString()))
            {
                yield return new ValidationResult("Monthly Clients Number is Required",
                      new[] { nameof(MonthlyClientTarget) });
            }

            //WordPress Block Validation
            if (String.IsNullOrEmpty(WpLoginUrl))
            {
                WpLoginUrl = "NA";
            }
            if (String.IsNullOrEmpty(WpPassword))
            {
                WpPassword = "NA";
            }
            if (String.IsNullOrEmpty(WpUserName))
            {
                WpUserName = "NA";
            }

            //Google Analytics Block Validation
            if (String.IsNullOrEmpty(GoogleAnalyticsUrl))
            {
                GoogleAnalyticsUrl = "NA";
            }
            if (String.IsNullOrEmpty(GoogleAnalyticsPassword))
            {
                GoogleAnalyticsPassword = "NA";
            }
            if (String.IsNullOrEmpty(GoogleAnalyticsUsername))
            {
                GoogleAnalyticsUsername = "NA";
            }

            //Google Search Console Block Validation
            if (String.IsNullOrEmpty(GoogleSearchConsolePassword))
            {
                GoogleSearchConsolePassword = "NA";
            }
            if (String.IsNullOrEmpty(GoogleSearchConsoleUrl))
            {
                GoogleSearchConsoleUrl = "NA";
            }
            if (String.IsNullOrEmpty(GoogleSearchConsoleUsername))
            {
                GoogleSearchConsoleUsername = "NA";
            }

            //Bing Webmaster Block Validation
            if (String.IsNullOrEmpty(BingWemasterToolsPassword))
            {
                BingWemasterToolsPassword = "NA";
            }
            if (String.IsNullOrEmpty(BingWemasterToolsUrl))
            {
                BingWemasterToolsUrl = "NA";
            }
            if (String.IsNullOrEmpty(BingWemasterToolsUsername))
            {
                BingWemasterToolsUsername = "NA";
            }

            //Google MyBusiness Block Validation
            if (String.IsNullOrEmpty(GoogleMyBusinessPassword))
            {
                GoogleMyBusinessPassword = "NA";
            }
            if (String.IsNullOrEmpty(GoogleMyBusinessUrl))
            {
                GoogleMyBusinessUrl = "NA";
            }
            if (String.IsNullOrEmpty(GoogleMyBusinessUsername))
            {
                GoogleMyBusinessUsername = "NA";
            }

            //Other fields Validation
            if (String.IsNullOrEmpty(SocialMedia))
            {
                SocialMedia = "NA";
            }
            if (BusinessDescription != null)
            {
                BusinessDescription = BusinessDescription.Trim();
            }
            
            //WebAddress = WebAddress.Trim();
            //DomainLoginUrl = DomainLoginUrl.Trim();
            //DomainUsername = DomainUsername.Trim();
            //DomainPassword = DomainPassword.Trim();
            //HostingLoginUrl = HostingLoginUrl.Trim();
            //HostingUserName = HostingUserName.Trim();
            //HostingPassword = HostingPassword.Trim();
            //WpLoginUrl = WpLoginUrl.Trim();
            //WpUserName = WpUserName.Trim();
            //WpPassword = WpPassword.Trim();
            //GoogleAnalyticsUrl = GoogleAnalyticsUrl.Trim();
            //GoogleAnalyticsUsername = GoogleAnalyticsUsername.Trim();
            //GoogleAnalyticsPassword = GoogleAnalyticsPassword.Trim();
            //GoogleSearchConsoleUrl = GoogleSearchConsoleUrl.Trim();
            //GoogleSearchConsoleUsername = GoogleSearchConsoleUsername.Trim();
            //GoogleSearchConsolePassword = GoogleSearchConsolePassword.Trim();
            //BingWemasterToolsUrl = BingWemasterToolsUrl.Trim();
            //BingWemasterToolsUsername = BingWemasterToolsUsername.Trim();
            //BingWemasterToolsPassword = BingWemasterToolsPassword.Trim();
            //GoogleMyBusinessUrl = GoogleMyBusinessUrl.Trim();
            //GoogleMyBusinessUsername = GoogleMyBusinessUsername.Trim();
            //GoogleMyBusinessPassword = GoogleMyBusinessPassword.Trim();
            //KeyWords = KeyWords.Trim();
            //TargetKeyPhases = TargetKeyPhases.Trim();
            //TargetAreas = TargetAreas.Trim();
            //CompetitorsUrl = CompetitorsUrl.Trim();
            //SocialMedia = SocialMedia.Trim();
            //SocialMedia2 = SocialMedia2.Trim();
            //SocialMedia3 = SocialMedia3.Trim();
            //SocialMedia4 = SocialMedia4.Trim();
            //OtherMarketingTypes = OtherMarketingTypes.Trim();
           
            //MarketingGoals = MarketingGoals.Trim();

            //ExpandPlaning = ExpandPlaning.Trim();

            yield return ValidationResult.Success;
        }

       

    }



    public class ClientMetadata 
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name is Required")]
        [Display(Name = "Client Name")]
        public int PersonId { get; set; }
        
        [Display(Name = "Business Description")]
        public string BusinessDescription { get; set; }
       
        public string WebAddress { get; set; }
       
        [Display(Name = "Domain Login Url")]
        public string DomainLoginUrl { get; set; }
       
        [Display(Name = "Domain Username")]
        public string DomainUsername { get; set; }
        
        [Display(Name = "Domain Password")]
        public string DomainPassword { get; set; }
        
        [Display(Name = "Hosting Url")]
        public string HostingLoginUrl { get; set; }
        
        [Display(Name = "Hosting Username")]
        public string HostingUserName { get; set; }
        
        [Display(Name = "Hosting Password")]
        public string HostingPassword { get; set; }
        [Display(Name = "WordPress Login Url")]
        public string WpLoginUrl { get; set; }
        [Display(Name = "WordPress Username")]
        public string WpUserName { get; set; }
        [Display(Name = "WordPress Password")]
        public string WpPassword { get; set; }
        [Display(Name = "Google Analytics Url")]
        public string GoogleAnalyticsUrl { get; set; }
        [Display(Name = "Google Analytics Username")]
        public string GoogleAnalyticsUsername { get; set; }
        [Display(Name = "Google Analytics Password")]
        public string GoogleAnalyticsPassword { get; set; }
        [Display(Name = "Google Search Console Url")]
        public string GoogleSearchConsoleUrl { get; set; }
        [Display(Name = "Google Search Console Username")]
        public string GoogleSearchConsoleUsername { get; set; }
        [Display(Name = "Google Search Console Password")]
        public string GoogleSearchConsolePassword { get; set; }
        [Display(Name = "Bing WebMaster Tools Url")]
        public string BingWemasterToolsUrl { get; set; }
        [Display(Name = "Bing WebMaster Tools Username")]
        public string BingWemasterToolsUsername { get; set; }
        [Display(Name = "Bing WebMaster Tools Password")]
        public string BingWemasterToolsPassword { get; set; }
        [Display(Name = "Google MyBusiness Url")]
        public string GoogleMyBusinessUrl { get; set; }
        [Display(Name = "Google MyBusiness Username")]
        public string GoogleMyBusinessUsername { get; set; }
        [Display(Name = "Google MyBusiness Password")]
        public string GoogleMyBusinessPassword { get; set; }
        [Display(Name = "What would you type into a Google search to find your company?")]
        public string KeyWords { get; set; }
        
        [Display(Name = "Target Key Phrases (Separate with commas)")]
        public string TargetKeyPhases { get; set; }
        
        [Display(Name = "Target Areas - Country/State/City (enter multiple locations if necessary)")]
        public string TargetAreas { get; set; }
        
        [Display(Name = "Competitor URLs (separate with commas)")]
        public string CompetitorsUrl { get; set; }
        [Display(Name = "Social Media URL 1 (ex: Facebook)")]
        public string SocialMedia { get; set; }
        [Display(Name = "Social Media URL 2")]
        public string SocialMedia2 { get; set; }
        [Display(Name = "Social Media URL 3")]
        public string SocialMedia3 { get; set; }
        [Display(Name = "Social Media URL 4")]
        public string SocialMedia4 { get; set; }
        [Display(Name = "What Other Types of Marketing Are You Doing?")]
        public string OtherMarketingTypes { get; set; }
        
        [Display(Name = "Total Monthly Marketing Budget Web & Print?")]
        public int MonthlyBudget { get; set; }
        
        [Display(Name = "How Many Clients are you looking for a Month? ")]
        public int? MonthlyClientTarget { get; set; }
        
        [Display(Name = "Do you plan to expand in the next 12 months?")]
        public int ExpandPlaning { get; set; }
        
        [Display(Name = "Tell Us More about your Marketing Goals and your business:")]
        public string MarketingGoals { get; set; }

        public int ClientActiveFlag { get; set; }

        public Person Person { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
