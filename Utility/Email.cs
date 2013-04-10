using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Utility
{
    /// <summary>
    /// 邮件。
    /// </summary>
    public class Email
    {
        public static void SendEmail()
        {
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = "smtp.126.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("hujaoadmin", "hujaoweb2011");
            //星号改成自己邮箱的密码
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new MailMessage("hujaoadmin@126.com", "ace2011@126.com");
            message.Subject = "胡椒网注册确认信";
            message.Body = "欢迎您成为胡椒网的一员，在此表示热烈欢迎。";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            //添加附件
            //Attachment data = new Attachment(@"c:\Service1.cs", System.Net.Mime.MediaTypeNames.Application.Octet);
            //message.Attachments.Add(data);
            try
            {
                client.Send(message);
                //MessageBox.Show("Email successfully send.");
            }
            catch (Exception)
            {
                //MessageBox.Show("Send Email Failed." + ex.ToString());

            }



        }
        public static bool SendEmailTo(string emailad)
        {
            try
            {
                System.Net.Mail.SmtpClient client = new SmtpClient();
                client.Host = "smtp.126.com";
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("hujaoadmin", "hujaoweb2011");
                //星号改成自己邮箱的密码
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.Mail.MailMessage message = new MailMessage("hujaoadmin@126.com", emailad);
                message.Subject = "胡椒网注册成功确认信";
                message.Body = "<html><body>欢迎您成为胡椒网的一员，在此表示热烈欢迎。<center><br/>站长：Ace<br/>" + System.DateTime.Now.ToShortDateString() + "</body></html>";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                //添加附件
                //Attachment data = new Attachment(@"c:\Service1.cs", System.Net.Mime.MediaTypeNames.Application.Octet);
                //message.Attachments.Add(data);
                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                //MessageBox.Show("Send Email Failed." + ex.ToString());
            }
        }
    }
}