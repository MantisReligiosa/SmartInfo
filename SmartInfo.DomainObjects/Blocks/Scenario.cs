using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks;

public class Scenario : DisplayBlock
{
    public Scenario() { }

    public Scenario(DisplayBlock source)
        : base(source) { }

    public ScenarioDetails Details { get; set; }

    internal override void CopyDetails(DisplayBlock source)
    {
        var sourceDetails = ((Scenario)source).Details;
        Details.CopyFrom(sourceDetails);
    }

    internal override DisplayBlock Clone()
    {
        return new Scenario(this);
    }
}