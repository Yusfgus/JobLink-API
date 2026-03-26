namespace JobLink.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id == Guid.Empty ? throw new ArgumentException("Id cannot be empty.", nameof(id)) : id;
    }
}
