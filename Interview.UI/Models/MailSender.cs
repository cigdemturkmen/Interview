using FluentEmail.Core;
using FluentEmail.Smtp;
using Interview.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.UI.Models
{
    public class MailSender
    {
        public async void SendEmail()
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient(host: "localhost")
            {
                EnableSsl = false,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Demos"
            });

            Email.DefaultSender = sender;



            var email = await Email
                .From(emailAddress: "cgdem.t@gmail.com")
                .To(emailAddress: "user@mail.com", name: "User")
                .Subject(subject: "Talebinizin Sonucu Hakkında")
                .Body(body: "")
                .SendAsync();
        }
    }
}
