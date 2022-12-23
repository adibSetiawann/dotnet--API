using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using Quartz;

namespace finalProjectApplication.DefaultServices.CustomerAppServices
{
    public class SendEmail : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("adibsetiawan1998@gmail.com"));
            email.To.Add(MailboxAddress.Parse("savenyong4@gmail.com"));
            var builder = new BodyBuilder();

            // In order to reference selfie.jpg from the html text, we'll need to add it
            // to builder.LinkedResources and then use its Content-Id value in the img src.
            var image = builder.LinkedResources.Add(
                @"/home/adib/Documents/live-code3/finalProjectApplication/test.png"
            );
            image.ContentId = MimeUtils.GenerateMessageId();

            // Set the html version of the message text
            builder.HtmlBody = string.Format(
                @"<p>Hey Alice,<br>
            <p>What are you up to this weekend? Monica is throwing one of her parties on
            Saturday and I was hoping you could make it.<br>

            <a href=""https://www.youtube.com/watch?v=rqHkc6dAkVQ&list=RDMMW28XvD2WOno&index=6"">test link</a>
            <p>Will you be my +1?<br>
            <p>-- Joey<br>
            <center><img src=""cid:{0}""></center>",
                image.ContentId
            );

            // We may also want to attach a calendar event for Monica's party...
            // builder.Attachments.Add(@"C:\Users\Joey\Documents\party.ics");

            // Now we just need to set the message body and we're done

            email.Subject = "Forgot Password ";
            email.Body = builder.ToMessageBody();
            // email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            // smtp.Authenticate("ella58@ethereal.email", "jmqZMB3s5snHgBJSWk");
            smtp.Authenticate("adibsetiawan1998@gmail.com", "credential.txt");
            smtp.Send(email);
            smtp.Disconnect(true);
            await Task.Run(() => (true, "Success"));
        }
    }
}
