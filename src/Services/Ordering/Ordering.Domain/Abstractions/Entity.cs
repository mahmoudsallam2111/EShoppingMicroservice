
namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T> where T : notnull
{
    public  T Id { get; set; }
    public  DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public  DateTime? ModifieddAt { get; set; }
    public string? ModifiedBy { get; set; }
}
