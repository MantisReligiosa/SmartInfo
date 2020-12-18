using DomainObjects.Blocks.Details;

namespace DomainObjects.Blocks
{
    public class Scenario : DisplayBlock
    {
        public Scenario()
            : base()
        {

        }

        public Scenario(Scenario source)
            : base(source)
        {

        }

        public ScenarioDetails Details { get; set; }

        internal override void CopyDetails(DisplayBlock source)
        {
            var sourceDetails = ((Scenario)source).Details;
            if (Details == null)
            {
                Details = new ScenarioDetails();
            }
            Details.CopyFrom(sourceDetails);
        }

        internal override DisplayBlock Clone()
        {
            return new Scenario(this);
        }
    }
}
