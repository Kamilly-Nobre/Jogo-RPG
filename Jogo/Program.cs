using System;
using System.Collections.Generic;

namespace Jogo
{
    class Program
    {
        static void Main(string[] args)
        {
            //EMOJIS
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //MUSICA
            AudioManager.TocarMusica("battle-of-the-dragons-8037.mp3");

            //MENU PRINCIPAL
            Game.MenuPrincipal();

            //PARA A MUSICA NO FIM DO JOGO
            AudioManager.PararMusica();
        }
    }
}