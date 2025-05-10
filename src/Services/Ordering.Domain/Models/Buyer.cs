using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Models;

public class Buyer : Entity, IAggregateRoot
{
   [Required]
    public string IdentityGuid { get; private set; }

    public string Name { get; private set; }


    protected Buyer()
    {
    }

    public Buyer(string identity, string name) : this()
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }
}
