using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dominio.DTO.PlayListDTO
{
    public class PlaylistCreateDTO
    {
        public string Nome { get; set; }
        public int UsuarioID { get; set; }
    }
}
