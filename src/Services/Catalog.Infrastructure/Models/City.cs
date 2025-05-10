using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Infrastructure.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int StateId { get; set; }
    [ForeignKey("StateId")]
    public State State { get; set; } = default!;
}

public class State
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}