using System.ComponentModel.DataAnnotations;

// TODO: Add Data Annotations Localization
public class AccountModel
{
    [Required(ErrorMessage = "ناوی تەواو پێویستە.")]
    public string FullName { get; set; }


    [Required(ErrorMessage = "کۆدی وڵات پێویستە.")]
    public string CountryCode { get; set; }

    [Required(ErrorMessage = "ژمارەی مۆبایل پێویستە.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "ژمارەی مۆبایل تەنها ژمارە بێت.")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "ئیمەیڵ پێویستە.")]
    [EmailAddress(ErrorMessage = "ئیمەیڵی دروست بنووسە.")]
    public string Email { get; set; }

    public bool isEmailNotifactionEnabled { get; set; }
}