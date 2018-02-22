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

            yield return ValidationResult.Success;
        }


    }



    public class ClientMetadata 
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name is Required")]
        [Display(Name = "Client Name")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Business Description is Required")]
        [Display(Name = "Business Description")]
        public string BusinessDescription { get; set; }
        [Required(ErrorMessage = "WebAddress is Required")]
        public string WebAddress { get; set; }
        [Required(ErrorMessage = "Domain Login Url is Required")]
        [Display(Name = "Domain Login Url")]
        public string DomainLoginUrl { get; set; }
        [Required(ErrorMessage = "Domain Username is Required")]
        [Display(Name = "Domain Username")]
        public string DomainUsername { get; set; }
        [Required(ErrorMessage = "Domain Password is Required")]
        [Display(Name = "Domain Password")]
        public string DomainPassword { get; set; }
        [Required(ErrorMessage = "Hosting Url is Required")]
        [Display(Name = "Hosting Url")]
        public string HostingLoginUrl { get; set; }
        [Required(ErrorMessage = "Hosting Username is Required")]
        [Display(Name = "Hosting Username")]
        public string HostingUserName { get; set; }
        [Required(ErrorMessage = "Hosting Password is Required")]
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
        [Required(ErrorMessage = "Target Key Phrases are Required")]
        [Display(Name = "Target Key Phrases (Separate with commas)")]
        public string TargetKeyPhases { get; set; }
        [Required(ErrorMessage = "Target Areas are Required")]
        [Display(Name = "Target Areas - Country/State/City (enter multiple locations if necessary)")]
        public string TargetAreas { get; set; }
        [Required(ErrorMessage = "Competitor URLs Are Required")]
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
        [Required(ErrorMessage = "Monthly Budget is Required")]
        [Display(Name = "Total Monthly Marketing Budget Web & Print?")]
        public int MonthlyBudget { get; set; }
        [Required(ErrorMessage = "Monthly Clients Number is Required")]
        [Display(Name = "How Many Clients are you looking for a Month? ")]
        public int? MonthlyClientTarget { get; set; }
        [Required(ErrorMessage = "Expand Planning field is Required")]
        [Display(Name = "Do you plan to expand in the next 12 months?")]
        public string ExpandPlaning { get; set; }
        
        [Display(Name = "Tell Us More about your Marketing Goals and your business:")]
        public string MarketingGoals { get; set; }

        public Person Person { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
