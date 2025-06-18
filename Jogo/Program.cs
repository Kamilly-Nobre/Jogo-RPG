using System;
using System.Collections.Generic;

namespace Jogo
{
    // Classe principal que inicia o jogo
    class Program
    {
        static void Main(string[] args)
        {
            // Habilita suporte a Unicode/emojis
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Inicia a música de fundo
            AudioManager.TocarMusica("battle-of-the-dragons-8037.mp3");

            // Inicia o menu principal do jogo
            Game.MenuPrincipal();

            // Para a música ao final do jogo
            AudioManager.PararMusica();
        }
    }
}
