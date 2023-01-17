namespace CityInfo.Api.Services
{
    public interface IMailServices
    {
        void Send(string subject, string message);
    }
}