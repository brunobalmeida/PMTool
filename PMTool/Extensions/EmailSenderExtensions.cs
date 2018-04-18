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
            return emailSender.SendEmailAsync(email, "Congratulations, you have joined PMTool.",

             $"Hi , <br><br>" +
             $"Welcome to the best PM tool available specifically for SEO Pros. It’s great to have you as part of our growing community from all over the world. <br><br>" +
             $"Now that you’re part of our private community, we want to ensure that you get the most possible value as quickly as possible.To kick things off here’s a very brief overview of myself and why I developed this tool with my team. <br><br>" +
             $"My name is Trevor Cherewka and I am a self-professed work-a-holic and serial entrepreneur.  Like most entrepreneurs I am spinning a dozen plates at any given time.I am also right-brained so using software to help organize my day always felt like an interruption plus I have a great business memory so I was always able to rely on my memory to make sure that deadlines were never missed. <br><br>" +
             $"Well, as the business grew and more clients were being onboarded I was finding that my memory was being pushed to the limits.Still no deadlines were missed but there were many sleepless nights getting everything done.something had to happen and that’s when the opportunity to develop my own app became real.I tried many other project management apps but they all seemed to add to my day and not take away from it. <br><br>" +
             $"What started out as something that I was going to use quickly became bigger and took on a life of it’s own.  To add a little more work to an already stressful deadline we also decided to preload in SEO templates so if you are new to the SEO world and have not figured out your own strategy then you can use one of the pre-installed templates. <br><br>" +
             $"Please make sure you check out our website http://smartsimpleseo.com to keep up-to-date on all the lates and greatest SEO news. <br><br>" +
             $"Please feel free to reach out if there is anything I can do for your. <br><br>" +
             $"Cheers, <br>" +
             $"Trevor <br><br>" +
             $"<h3>Getting Started</h3> <br>" +
             $"First things first, you’re going to need to visit the PMTool YouTube channel to make sure you are getting the most out of the app.This is where all of the activity happens, including live Q&A sessions with SEO leaders. <br><br>" +
             $"Next you are going to want to fill out the admin section and then add any team members to your app. <br><br>" +
             $"Now that all the admin is out of the way, let’s move on to growing your business... <br><br>" +
             $"<h3>Getting Maximum Value</h3> <br>" +
             $"As part of our community, we want to ensure that you get the absolute maximum value possible from your experience with us.To do so, we’d recommend doing the following things: <br><br>" +
             $"<b>Ask Us Questions:</b> we mean it when we say that you can ask us questions at any time.We’ll always respond within 24 hours.It doesn’t matter how complex or simple the question is. <br><br>" +
             $"We’re all here to help you and your business grow, so make sure you let us know if there’s anything else you’d love to see us offer to the community by dropping me a line. <br><br>" +
             $"See you inside!" +

             $" <a type='button' href='{HtmlEncoder.Default.Encode(link)}'>Click to Register</a>");
        }

        //Not currently in use. Saved for posterior upgrade
        //public static Task SendEmailRegistrationEmployeeAsync(this IEmailSender emailSender, string email, string link)
        //{
        //    return emailSender.SendEmailAsync(email, "You have been registered for PMTool.",
        //     $"Please click on the link to Register into our system: \n\n\n" +
        //     $" <a type='button' href='{HtmlEncoder.Default.Encode(link)}'>Click to Register</a>");
        //}

    }
}
