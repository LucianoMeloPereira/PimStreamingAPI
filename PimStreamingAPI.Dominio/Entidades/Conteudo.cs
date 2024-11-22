using System;

namespace PimStreamingAPI.Dominio.Entidades
{
    public class Conteudo
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; } // Por exemplo: "Video", "Audio"
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
        public int CriadorID { get; set; }
        public Usuario Criador { get; set; }

        // Novo campo para o caminho do arquivo de vídeo
        public string CaminhoArquivo { get; set; }
    }
}
