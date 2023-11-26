using System.Net.Mail;
using System.Net;

namespace Parking_Garage_Application.Gmail
{
    public class InvoiceSender
    {
        private string senderEmail = "parkinggarages3@gmail.com";

        public void SendInvoice(string toEmail, double price)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, "grepbokafmsqomse"),
                EnableSsl = true,
            };

            MailMessage message = new MailMessage(senderEmail, toEmail)
            {
                Subject = "Parking garage payment accepted",
                IsBodyHtml = true,
                Body = $@"
                    <html>
                        <body>
                            <p>Dear Customer,</p>
                            <p>Thank you for choosing our parking garage. Your payment of ${price:F2} has been accepted.</p>
                            <p>We appreciate your business and hope you had a pleasant stay.</p>
                            <p>Thanks for choosing us!</p>
                            <p>Best regards,<br>Your Parking Garage Team</p>
                        </body>
                    </html>"
            };
        
            smtpClient.Send(message);
        }
    }
}
