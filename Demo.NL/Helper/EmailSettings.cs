using Demo.DAL.Entites;
using System.Net;
using System.Net.Mail;

namespace Demo.NL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("abdallahzeyad337@gmail.com", "vxcrgzflsgqiorva");
            client.Send("abdallahzeyad337@gmail.com", email.To, email.Title, email.Body);

        }
    }
}
