using System.ComponentModel.DataAnnotations;

// TODO: Add Data Annotations Localization
public class LoginModel
{
    [Required(ErrorMessage = "کۆدی وڵات پێویستە.")]
    public string CountryCode { get; set; }

    [Required(ErrorMessage = "ژمارەی مۆبایل پێویستە.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "ژمارەی مۆبایل تەنها ژمارە بێت.")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "تێپەڕەوشە پێویستە.")]
    [MinLength(8, ErrorMessage = "تێپەڕەوشە دەبێت لە کەمترینەوە ٨ پیت بێت.")]
    public string Password { get; set; }
}