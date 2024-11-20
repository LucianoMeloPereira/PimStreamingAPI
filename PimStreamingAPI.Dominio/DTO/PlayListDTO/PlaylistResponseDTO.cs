using PimStreamingAPI.DTO.ConteudoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dominio.DTO.PlayListDTO
{
    public class PlaylistResponseDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int UsuarioID { get; set; }
        public List<ConteudoResponseDTO> Conteudos { get; set; }    }
}
