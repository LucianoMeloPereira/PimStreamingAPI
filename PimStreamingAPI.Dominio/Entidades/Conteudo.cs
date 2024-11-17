using System;

namespace PimStreamingAPI.Dominio.Entidades
{
    public class Conteudo
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public int CriadorID { get; set; }

        // Permitir valores nulos para a chave estrangeira PlaylistID
        public int? PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
    }
}
