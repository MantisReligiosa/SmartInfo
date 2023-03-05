namespace DomainObjects;

public class Identity
{
    public Guid Id { get; set; }

    public Identity()
    {
        Id = Guid.NewGuid();
    }
}