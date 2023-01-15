namespace CityInfo.Api.Services
{
    public class LocalMailServices
    {
        private string _mailTo = "admin@mycompany.com";
        private string _mailFrom = "noreplay@mycompany.com";

        public void Send(string subject, string message)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo},  with {nameof(LocalMailServices)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
