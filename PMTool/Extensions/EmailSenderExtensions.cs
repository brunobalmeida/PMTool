using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using PMTool.Services;

namespace PMTool.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static Task SendEmailRegistrationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Congratulations, you have bought a copy of our software.",
             $"Please click on the link to Register into our system: \n\n\n" +
             $" <a type='button' href='{HtmlEncoder.Default.Encode(link)}'>Click to Register</a>");
        }

        public static Task SendEmailRegistrationEmployeeAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "You have been registered for PMTool.",
             $"Please click on the link to Register into our system: \n\n\n" +
             $" <a type='button' href='{HtmlEncoder.Default.Encode(link)}'>Click to Register</a>");
        }

    }
}
