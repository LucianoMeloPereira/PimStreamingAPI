﻿using System;
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

        // Relacionamento
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Conteudo> Conteudos { get; set; }
    }
}
