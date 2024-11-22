using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dominio.DTO.ConteudoDTO
{
    public class ConteudoUploadDTO
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public int PlaylistID { get; set; }
        public int CriadorID { get; set; }
        public IFormFile Arquivo { get; set; } // Arquivo de vídeo
    }
}
