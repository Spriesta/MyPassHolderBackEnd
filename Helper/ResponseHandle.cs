namespace MyPassHolder.Common
{
    public class ResponseHandle
    {
        public Boolean success { get; set; } = true;
        public object? data { get; set; }
        public string? errorMesssage { get; set; }
    }
}
