namespace PimStreamingAPI.DTO.ConteudoDTO
{
    public class ConteudoResponseDTO
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public int PlaylistID { get; set; }
        public int CriadorID { get; set; }

        // Propriedade adicionada para resolver o erro
        public string CaminhoArquivo { get; set; }
    }
}
