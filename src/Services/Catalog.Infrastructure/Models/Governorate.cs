namespace Catalog.Infrastructure.Models;
public class Governorate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}