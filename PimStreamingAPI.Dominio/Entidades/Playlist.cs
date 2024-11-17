using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dominio.Entidades
{
    public class Playlist
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public List<Conteudo> Conteudos { get; set; }
    }
}
