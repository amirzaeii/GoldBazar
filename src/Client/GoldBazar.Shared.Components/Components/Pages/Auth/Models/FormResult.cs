public class FormResult
{
    public bool Success { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string>? Data { get; set; } = null;
}