using System.Collections.Generic;

namespace Web.Models.Blocks
{
    public class ScenarioDto : BlockDto
    {
        public List<SceneDto> Scenes { get; set; }
    }
}