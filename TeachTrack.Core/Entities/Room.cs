using TeachTrack.Core.Exceptions;
using TeachTrack.Core.Interface;

namespace TeachTrack.Core.Entities;

public class Room : Entity, IAggregateRoot {
    // Changed to string to match SQL VARCHAR(20) and support "101B"
    public string RoomNumber { get; private set; } 
    public string Building { get; private set; }
    public int Capacity { get; private set; }

    // EF Core Constructor
    private Room() { }

    public Room(string roomNumber, string building, int capacity) {
        if (string.IsNullOrWhiteSpace(roomNumber)) 
            throw new DomainException("Room number is required.");
            
        if (string.IsNullOrWhiteSpace(building))
            throw new DomainException("Building name is required.");
            
        if (capacity <= 0)
            throw new DomainException("Capacity must be greater than zero.");
        
        Id = Guid.NewGuid();
        RoomNumber = roomNumber;
        Building = building;
        Capacity = capacity;
    }

    // Optional: Domain Behavior
    public void UpdateCapacity(int newCapacity) {
        if (newCapacity <= 0)
            throw new DomainException("Capacity must be positive.");
        Capacity = newCapacity;
    }
}