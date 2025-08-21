using System.ComponentModel.DataAnnotations;

// TODO: Add Data Annotations Localization
public class OtpModel
{
    [Required(ErrorMessage = "کۆدی OTP پێویستە.")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "کۆدی OTP دەبێت تەنها ٤ ژمارە بێت.")]
    public string Code { get; set; }
}