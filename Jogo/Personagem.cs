using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    // Representa um personagem do jogo (jogador ou aliados)
    class Personagem
    {
        public string Nome;
        public string Arma;
        public int Vida;
        public int VidasExtras;
        public int Pocoes;
        public int Moedas;
        public int Ataque;

        // Construtor que inicializa o personagem com valores padrão
        public Personagem(string nome, string arma)
        {
            Nome = nome;
            Arma = arma;
            Vida = 100;
            VidasExtras = 1;
            Pocoes = 1;
            Moedas = 10;
            Ataque = 15;
        }
    }
}
