﻿namespace Ordering.Infrastructure.Idempotency;

public class ClientRequest
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public DateTime Time { get; set; }
}
