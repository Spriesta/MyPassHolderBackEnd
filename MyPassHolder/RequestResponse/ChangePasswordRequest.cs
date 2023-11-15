namespace MyPassHolder.RequestResponse
{
    public class ChangePasswordRequest
    {
        public string token { get; set; }
        public string newPassword { get; set; }
    }
}
