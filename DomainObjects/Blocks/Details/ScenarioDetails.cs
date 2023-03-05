namespace DomainObjects.Blocks.Details;

public class ScenarioDetails : Identity, ICopyable<ScenarioDetails>
{
    public ICollection<Scene> Scenes { get; set; }

    public ScenarioDetails(ScenarioDetails source)
    {
        CopyFrom(source);
    }

    public void CopyFrom(ScenarioDetails source) =>
        Scenes = source.Scenes.Select(_ => new Scene(_)).ToList();
}