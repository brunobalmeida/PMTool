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
