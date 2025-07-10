using System.ComponentModel.DataAnnotations;

// TODO: Add Data Annotations Localization
public class ForgotPasswordModel
{
    [Required(ErrorMessage = "کۆدی وڵات پێویستە.")]
    public string CountryCode { get; set; }

    [Required(ErrorMessage = "ژمارەی مۆبایل پێویستە.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "ژمارەی مۆبایل تەنها ژمارە بێت.")]
    public string Mobile { get; set; }
}