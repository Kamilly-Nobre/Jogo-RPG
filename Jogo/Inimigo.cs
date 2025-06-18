using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    // Representa um inimigo no jogo
    class Inimigo
    {
        public string Nome;
        public int Vida;
        public int Dano;

        // Construtor do inimigo
        public Inimigo(string nome, int vida, int dano)
        {
            Nome = nome;
            Vida = vida;
            Dano = dano;
        }
    }
}
