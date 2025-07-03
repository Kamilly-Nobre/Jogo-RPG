using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    class Inimigo
    {
        public string Nome { get; set; }
        public int Vida { get; set; }
        public int DanoMin { get; set; }
        public int DanoMax { get; set; }

        private static readonly Random rng = new Random();

        public Inimigo(string nome, int vida, int danoMin, int danoMax)
        {
            Nome = nome;
            Vida = vida;
            DanoMin = danoMin;
            DanoMax = danoMax;
        }

        public int GerarDano()
        {
            return rng.Next(DanoMin, DanoMax + 1);
        }
    }
}