using System.Collections.Generic;

namespace DomainObjects.Blocks.Details
{
    public class ScenarioDetails : Identity, ICopyable<ScenarioDetails>
    {
        public ICollection<Scene> Scenes { get; set; }

        public ScenarioDetails() { }

        public ScenarioDetails(ScenarioDetails source)
        {
            CopyFrom(source);
        }

        public void CopyFrom(ScenarioDetails source)
        {
            Scenes = new List<Scene>();
            foreach(var frame in source.Scenes)
            {
                Scenes.Add(new Scene(frame));
            }
        }
    }
}
