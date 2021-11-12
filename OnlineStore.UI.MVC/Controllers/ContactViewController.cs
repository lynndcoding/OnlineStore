using OnlineStore.Models;
using OnlineStore.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.UI.MVC.Controllers
{
    public class ContactViewController : Controller
    {
        // GET: ContactView
        public ActionResult Index()
        {
            return View();
        }
        #region Contact Page Region
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {

            //When a class has validation attributes, that validation should be 
            //checked BEFORE attempting to process any data



            //If the ModelState is not valid (it failed validation)
            if (!ModelState.IsValid)
            {
                return View(cvm); //Passing in cvm will populate the form with all the user typed in before they submitted the request.
            }
            //


            #endregion

            #region Create a Format for the Message to be Emailed

            //The rest of the code in this action only executes if the form (cvm object) passes
            //the model validation.
            string message = $"You have received an email from {cvm.Name} with a subject {cvm.Subject}. " +
                $"Please respond to {cvm.Email} with your response to the following message: <br />{cvm.Message}";

            #endregion

            #region Create the MailMessage object and set up the (from, to, subject, body)


            //MailMessage object (what actually sends the email) - System.Net.Mail
            MailMessage mm = new MailMessage(

                //FROM
                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                //TO
                //this assumes email forwarding by the host (which we set up for this email user)
                ConfigurationManager.AppSettings["EmailTo"].ToString(),

                //SUBJECT
                cvm.Subject,

                //BODY / MESSAGE
                message

                );

            #endregion

            #region (Optional) Customize MailMessage Properties

            //MailMessage properties
            //Allow HTML formatting in the email (our message format has HTML in it)
            mm.IsBodyHtml = true;

            //If you want to mark these emails with high priority
            mm.Priority = MailPriority.High; //the default behavior is Normal priority

            //Respond to the sender's email instead of our own webmail on SmarterASP
            mm.ReplyToList.Add(cvm.Email);

            #endregion

            #region Create the SmtpClient object and provide the credentials

            //SMTP - Secure Mail Transfer Protocol
            //SmtpClient - This is the information from the HOST (SmarterASP.Net) which
            //allows the email to actually be sent.
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client credentials (SmarterASP requires your username and password to send emails)
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString());
            //client.Port = 25;
            //client.Port = 8889;
            #endregion

            #region Wrap it in a try/catch

            //It is possible that the mailserver for SmarterASP is down when the user attempts
            //to send us a message. It's also possible that we may have configuration issues (typos, 
            //missing keys, wrong passwords, etc.). In the event something goes wrong, we want to 
            //be able to handle that and let the user know (without having to crash the app)
            //So, we can encapsulate our code in a try/catch

            try
            {
                //Attempt to send the email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time. " +
                    $"Please try again later. Error Message: <br />{ex.StackTrace}";

                //return the View with the entire message so the user can copy/paste it for later.
                return View(cvm);
            }

            #endregion

            #region Return the View

            //If everything goes well, we end up here. So, we can return a View that displays a 
            //confirmation to the user so they know their email was sent.

            return View("EmailConfirm", cvm);

            #endregion
        }
    }
}