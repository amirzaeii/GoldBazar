
using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure.Models;
public class Manufacture
{
    public int Id { get; set; }
    public int Karat { get; set; }
    public decimal Purity { get; set; }
    public string Name { get; set; } = string.Empty;
    public ManufactureSource Source { get; set; }

}
public enum ManufactureSource
{
    [Display(Name = "Dubai")]
    Dubai = 1,
    [Display(Name = "Turkey")]
    Turkey = 2,    
    [Display(Name = "Iraq")]
    Iraq = 3,   
    [Display(Name = "Other")]
    Other = 4,   

}
