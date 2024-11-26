﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimStreamingAPI.Dominio.Entidades
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        // Relacionamento
        public ICollection<Playlist> Playlists { get; set; }
    }
}
