namespace MyPassHolder.RequestResponse
{
    public class RegisterRequest
    {
        public string fullName { get; set; } = null!;
        public string password { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
    }
}
