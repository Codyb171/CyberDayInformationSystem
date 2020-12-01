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

                //send(); - I dunno if we need this
            }
        }

        //sendTo: the recipient's email
        //subject: email's subject line
        //body: the email's body
        public static Boolean send(string sendTo, string subject, string body)
        {
            //http://csharp.net-informations.com/communications/csharp-email-attachment.htm

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("cyberdayjmuofficial@gmail.com");
                mail.To.Add(sendTo); // dynamically add recipients here.  
                mail.Bcc.Add("cyberdayjmuofficial@gmail.com");
                mail.Subject = subject;
                mail.Body = body;

                // could attach brochure to send out promotions, paperwork for registered participants
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cyberdayjmuofficial", "CyberDay!sGre@t"); // need to protect this!
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }
    }
}