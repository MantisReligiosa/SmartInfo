namespace Web.Models.Blocks.Converter
{
    public static class BlockIdProcessor
    {
        public static string GetIdDTO(string blockType, int id)
        {
            return $"{blockType}_{id:d4}";
        }

        public static int FromDTOId(string dtoId)
        {
            var idString = dtoId.Substring(dtoId.Length - 4);
            return int.Parse(idString);
        }
    }
}