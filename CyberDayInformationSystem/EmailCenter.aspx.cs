using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;

namespace CyberDayInformationSystem
{
    public partial class EmailCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                send();
            }
        }

        protected void send()
        {

            //http://csharp.net-informations.com/communications/csharp-email-attachment.htm

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("cyberdayjmuofficial@gmail.com");
                mail.To.Add("cyberdayjmuofficial@gmail.com");
                mail.Subject = "Test Mail - 2";
                mail.Body = "Dear [variable name], \n" +
                    "We are emailing in preperation of our upcoming event on [variable name]. Please complete the forms" +
                    "attached to this email prior to [variable name]. If you have any questions, email [cooridnator email]. \n\n" +
                    "Sincerely, \n" +
                    "Dr. Dillon & Dr. Lough";

                // could attach brochure to send out promotions, paperwork for registered participants
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cyberdayjmuofficial", "CyberDay!sGre@t"); // need to protect this!
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                // add confirmation
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}