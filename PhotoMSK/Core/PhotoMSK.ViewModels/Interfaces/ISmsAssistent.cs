using System.Threading.Tasks;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface ISmsAssistent
    {
        Task<string> SendMessageAsync(string phone, string text);
        string SendMessage(string phone, string text);
    }
}