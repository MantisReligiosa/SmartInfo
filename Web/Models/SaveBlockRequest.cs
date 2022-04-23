using Web.Models.Blocks;

namespace Web.Models
{
    public class SaveBlockRequest<T>
        where T : BlockDto
    {
        public T Block { get; set; }
    }
}