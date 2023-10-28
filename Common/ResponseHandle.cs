namespace Common
{
    public class ResponseHandle
    {
        public bool success { get; set; } = true;
        public object? data { get; set; }
        public string? errorMesssage { get; set; }
    }
}
