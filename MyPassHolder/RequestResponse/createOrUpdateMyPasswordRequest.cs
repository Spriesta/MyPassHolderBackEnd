namespace MyPassHolder.RequestResponse
{
    public class createOrUpdateMyPasswordRequest
    {
        public long? id { get; set; }
        public long userId { get; set; }
        public long categoryId { get; set; }
        public string description { get; set; } = "";
        public string password { get; set; } = "";
    }
}
