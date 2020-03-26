using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "mohamed.bennourddine@gmail.com";
        public string MailFromAddress = "mohamed.bennourddine@gmail.com";
        public bool UseSsl = true;
        public string Username = "mohamed.bennourddine@gmail.com";
        public string Password = "mornag1256*";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\orders_bookstore_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSetting;
        public EmailOrderProcessor(EmailSettings setting)
        {
            emailSetting = setting;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSetting.UseSsl;
                smtpClient.Host = emailSetting.ServerName;
                smtpClient.Port = emailSetting.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new
                                                        NetworkCredential(emailSetting.Username, emailSetting.Password);
                if (emailSetting.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                                                .AppendLine("A new order has been submitted ")
                                                .AppendLine("---------------------------------------------------------------------------------------------------------")
                                                .AppendLine("Books: ");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat("{0}x{1}(subtotal:{2:c}) /",
                                                                        line.Quantity, line.Book.Title, subtotal);
                }
                body.AppendLine(" ").
                         AppendFormat("Total order value:{0:c} ", cart.ComputeTotalValue())
                        .AppendLine("")
                        .AppendLine("---------------------------------------------------------------------------------------------------------")
                        .AppendLine("Ship to :")
                        .AppendLine(shippingDetails.Name)
                        .AppendLine(shippingDetails.Line1)
                        .AppendLine(shippingDetails.Line2)
                        .AppendLine(shippingDetails.State)
                        .AppendLine(shippingDetails.City)
                         .AppendLine(shippingDetails.Country)
                         .AppendLine("-------------------------------------")
                         .AppendFormat("Gift Wrap:{0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                                                              emailSetting.MailFromAddress,
                                                              shippingDetails.Email,
                                                              "New order submitted",
                                                              body.ToString());
                if (emailSetting.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.ASCII;
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }
    }
}
