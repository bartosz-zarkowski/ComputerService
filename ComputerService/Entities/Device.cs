﻿namespace ComputerService.Entities;
public class Device : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Password { get; set; }
    public string? Condition { get; set; }
    public bool HasWarranty { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
}