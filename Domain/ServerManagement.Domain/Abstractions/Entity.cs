namespace ServerManagement.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public required T Id { get; set; }
}
