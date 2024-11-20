namespace PimStreamingAPI.DTO.ConteudoDTO
{
    public class ConteudoCreateDTO
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public int PlaylistID { get; set; }
        public int CriadorID { get; set; }
    }
}
