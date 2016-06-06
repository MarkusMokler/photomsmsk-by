namespace PhotoMSK.ViewModels
{
    public class ResponseViewModel
    {
        public string Message { get; set; }
    }

    public class ErrorViewModel : ResponseViewModel
    {
        public int Error { get; set; }
        public string[] ErrorFields { get; set; }
    }
}