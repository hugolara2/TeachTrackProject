using System.Reflection.Metadata;

namespace TeachTrack.Core.Entities;

public abstract class Entity : IEquatable<Entity> {
    public Guid Id { get; protected set; }
    
    protected Entity() {}
    protected Entity(Guid id) => Id = id;

    public bool Equals(Entity? other) {
        if (other is null) return false;
        
        // Reference equality check (optimization)
        if (ReferenceEquals(this, other)) return true;

        // Identity equality check
        if (this.GetType() != other.GetType()) return false;
        
        return Id == other.Id;
    }
    
    public override bool Equals(object? obj) => Equals(obj as Entity);
    
    public override int GetHashCode() => Id.GetHashCode();
}