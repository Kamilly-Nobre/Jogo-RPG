using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    class Personagem
    {
        public string Nome;
        public string Arma;
        public int Vida;
        public int VidasExtras;
        public int Pocoes;
        public int Moedas;
        public int Ataque;

        public Personagem(string nome, string arma)
        {
            Nome = nome;
            Arma = arma;
            Vida = 100;
            VidasExtras = 1;
            Pocoes = 1;
            Ataque = 15;
        }

        public int CustoMelhoriaArma = 15;
        public int VidaMaxima { get; set; } = 100;
        public int FragmentosDaAlma { get; set; } = 1;

    }
}