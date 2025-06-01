using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Infrastructure.Models;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int GovernorateId { get; set; }
    [ForeignKey("GovernorateId")]
    public Governorate Governorate { get; set; } = default!;
}

