﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.DTO.Usuario
{
    public class UsuarioBuscaDTO
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public bool Ativo { get; set; }
        public string Role { get; set; }
    }
}
