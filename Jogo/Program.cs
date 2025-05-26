using System;
using System.Collections.Generic;

namespace Jogo
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.MenuPrincipal();
        }
    }

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
            VidasExtras = 2;
            Pocoes = 0;
            Moedas = 0;
            Ataque = 20;
        }
    }

    class Inimigo
    {
        public string Nome;
        public int Vida;
        public int Dano;

        public Inimigo(string nome, int vida, int dano)
        {
            Nome = nome;
            Vida = vida;
            Dano = dano;
        }
    }

    class Game
    {
        public static List<Personagem> grupo = new List<Personagem>();
        public static int ouro = 0;
        public static string[] mundos = { "Floresta Sombria", "Deserto Ardente", "Montanhas Congeladas", "Ruínas Perdidas" };

        public static void MenuPrincipal()
        {
            Console.WriteLine(" Bem-vindo ao Jogo Dimensões Quebradas!");
            Console.WriteLine("\nEscolha o sexo do personagem principal (1 - Mulher, 2 - Homem):");
            string escolha = Console.ReadLine();

            Console.Write("Digite o nome do seu personagem principal: ");
            string nomePrincipal = Console.ReadLine();

            if (escolha == "1")
            {
                grupo.Add(new Personagem(nomePrincipal, "Espada"));
                grupo.Add(new Personagem("Zarek", "Arco e Flecha"));
                grupo.Add(new Personagem("Nira", "Livro de Magias"));
                grupo.Add(new Personagem("Robô", "Raio Laser"));
            }
            else
            {
                grupo.Add(new Personagem(nomePrincipal, "Espada"));
                grupo.Add(new Personagem("Nira", "Arco e Flecha"));
                grupo.Add(new Personagem("Zarek", "Livro de Magias"));
                grupo.Add(new Personagem("Robô", "Raio Laser"));
            }

            Console.WriteLine("\nA aventura começou!");
            MostrarStatus();
            Pausa();
            IniciarMundos();
        }

        public static void IniciarMundos()
        {
            for (int i = 0; i < mundos.Length; i++)
            {
                Console.WriteLine($"\n=== Mundo: {mundos[i]} ===");
                Console.WriteLine($"Herói principal aqui: {grupo[i].Nome} com sua arma: {grupo[i].Arma}");
                Pausa();
                List<Inimigo> inimigos = GerarInimigos(mundos[i]);

                foreach (var inimigo in inimigos)
                {
                    Combate(grupo[i], inimigo);
                    MostrarStatus();
                    Pausa();
                }

                Inimigo boss = mundos[i] switch
                {
                    "Floresta Sombria" => new Inimigo("Guardião da Árvore Negra", 60, 15),
                    "Deserto Ardente" => new Inimigo("Faraó das Cinzas", 60, 15),
                    "Montanhas Congeladas" => new Inimigo("Rainha da Tempestade", 60, 15),
                    "Ruínas Perdidas" => new Inimigo("Guardião do Tempo Esquecido", 60, 15),
                    _ => new Inimigo($"Chefe de {mundos[i]}", 60, 15)
                };
                Console.WriteLine($"\nChefe apareceu: {boss.Nome}!");
                Pausa();
                Combate(grupo[i], boss);
                MostrarStatus();
                Pausa();
                Loja();
            }

            MundoFinal();
        }

        public static List<Inimigo> GerarInimigos(string mundo)
        {
            List<Inimigo> inimigos = new List<Inimigo>();

            switch (mundo)
            {
                case "Floresta Sombria":
                    inimigos.Add(new Inimigo("Sombra Espreitadora", 30, 10));
                    inimigos.Add(new Inimigo("Lobisomem das Folhas", 30, 10));
                    inimigos.Add(new Inimigo("Esporo Venenoso", 30, 10));
                    break;
                case "Deserto Ardente":
                    inimigos.Add(new Inimigo("Escorpião de Fogo", 30, 10));
                    inimigos.Add(new Inimigo("Ladrão das Dunas", 30, 10));
                    inimigos.Add(new Inimigo("Serpente Solar", 30, 10));
                    break;
                case "Montanhas Congeladas":
                    inimigos.Add(new Inimigo("Golem de Gelo", 30, 10));
                    inimigos.Add(new Inimigo("Yeti Selvagem", 30, 10));
                    inimigos.Add(new Inimigo("Nevasca Viva", 30, 10));
                    break;
                case "Ruínas Perdidas":
                    inimigos.Add(new Inimigo("Espectro Antigo", 30, 10));
                    inimigos.Add(new Inimigo("Sentinela de Pedra", 30, 10));
                    inimigos.Add(new Inimigo("Múmia Desperta", 30, 10));
                    break;
            }

            return inimigos;
        }

        public static void Combate(Personagem heroi, Inimigo inimigo)
        {
            Console.WriteLine($"\n{heroi.Nome} encontrou {inimigo.Nome}!");
            Pausa();

            while (inimigo.Vida > 0 && (heroi.Vida > 0 || heroi.VidasExtras > 0))
            {
                Console.WriteLine($"\nVida de {heroi.Nome}: {heroi.Vida} | Vida do Inimigo: {inimigo.Vida}");
                Console.WriteLine("Escolha uma ação: 1-Atacar 2-Defender 3-Usar Poção");
                string acao = Console.ReadLine();

                switch (acao)
                {
                    case "1":
                        Console.WriteLine($"{heroi.Nome} atacou com {heroi.Arma}!");
                        inimigo.Vida -= heroi.Ataque;
                        break;
                    case "2":
                        Console.WriteLine($"{heroi.Nome} defendeu!");
                        continue;
                    case "3":
                        if (heroi.Pocoes > 0)
                        {
                            heroi.Vida = Math.Min(100, heroi.Vida + 50);
                            heroi.Pocoes--;
                            Console.WriteLine($"{heroi.Nome} usou uma poção! Vida agora: {heroi.Vida}");
                        }
                        else Console.WriteLine("Sem poções!");
                        continue;
                }

                if (inimigo.Vida > 0)
                {
                    Console.WriteLine($"{inimigo.Nome} atacou {heroi.Nome}!");
                    heroi.Vida -= inimigo.Dano;
                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0)
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = 100;
                        Console.WriteLine($"{heroi.Nome} perdeu uma vida extra!");
                    }
                }
                Pausa();
            }

            if (inimigo.Vida <= 0)
            {
                Console.WriteLine($"{inimigo.Nome} derrotado!");
                ouro += 20;
                heroi.Moedas += 20;
            }
        }

        public static void Loja()
        {
            Console.WriteLine("\nBem-vindo à loja!");

            foreach (var heroi in grupo)
            {
                Console.WriteLine($"\nLoja para {heroi.Nome}!");

                bool continuarComprando = true;
                while (continuarComprando)
                {
                    Console.WriteLine($"Moedas disponíveis: {heroi.Moedas}");
                    Console.WriteLine("Escolha uma opção:");
                    Console.WriteLine("1. Comprar Poção de Vida (+50 de Vida) - 10 moedas");
                    Console.WriteLine("2. Melhorar Arma (+5 de Ataque) - 15 moedas");
                    Console.WriteLine("3. Sair");

                    string escolha = Console.ReadLine();

                    switch (escolha)
                    {
                        case "1":
                            if (heroi.Moedas >= 10)
                            {
                                heroi.Pocoes++;
                                heroi.Moedas -= 10;
                                Console.WriteLine($"{heroi.Nome} comprou uma Poção de Vida!");
                            }
                            else
                            {
                                Console.WriteLine("Moedas insuficientes!");
                            }
                            break;

                        case "2":
                            if (heroi.Moedas >= 15)
                            {
                                heroi.Ataque += 5;
                                heroi.Moedas -= 15;
                                Console.WriteLine($"{heroi.Nome} melhorou sua Arma!");
                            }
                            else
                            {
                                Console.WriteLine("Moedas insuficientes!");
                            }
                            break;

                        case "3":
                            continuarComprando = false;
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            }
        }

        public static void MundoFinal()
        {
            Console.WriteLine("\n--- MUNDO FINAL ---");
            Console.WriteLine("Todos os heróis se unem contra o Boss Final: Mestre da Ilusão!");
            Pausa();

            Inimigo bossFinal = new Inimigo("Mestre da Ilusão", 200, 25);

            while (bossFinal.Vida > 0 && grupo.Exists(p => p.Vida > 0 || p.VidasExtras > 0))
            {
                foreach (var heroi in grupo)
                {
                    if (heroi.Vida <= 0 && heroi.VidasExtras == 0) continue;
                    if (bossFinal.Vida <= 0) break;

                    Console.WriteLine($"\nTurno de {heroi.Nome}:");
                    Console.WriteLine("1-Atacar 2-Defender 3-Usar Poção");
                    string acao = Console.ReadLine();

                    switch (acao)
                    {
                        case "1":
                            Console.WriteLine($"{heroi.Nome} atacou com {heroi.Arma}!");
                            bossFinal.Vida -= heroi.Ataque;
                            break;
                        case "2":
                            Console.WriteLine($"{heroi.Nome} defendeu!");
                            continue;
                        case "3":
                            if (heroi.Pocoes > 0)
                            {
                                heroi.Vida = Math.Min(100, heroi.Vida + 50);
                                heroi.Pocoes--;
                                Console.WriteLine($"{heroi.Nome} usou uma poção!");
                            }
                            else Console.WriteLine("Sem poções!");
                            continue;
                    }

                    if (bossFinal.Vida > 0)
                    {
                        Personagem alvo = grupo[new Random().Next(grupo.Count)];
                        while (alvo.Vida <= 0 && alvo.VidasExtras == 0)
                            alvo = grupo[new Random().Next(grupo.Count)];

                        Console.WriteLine($"\n{bossFinal.Nome} atacou aleatoriamente {alvo.Nome}!");
                        alvo.Vida -= bossFinal.Dano;
                        if (alvo.Vida <= 0 && alvo.VidasExtras > 0)
                        {
                            alvo.VidasExtras--;
                            alvo.Vida = 100;
                            Console.WriteLine($"{alvo.Nome} usou uma vida extra!");
                        }
                    }
                    Pausa();
                }
                MostrarStatus();
            }

            if (bossFinal.Vida <= 0)
            {
                Console.WriteLine("\nVOCÊS DERROTARAM O MESTRE DA ILUSÃO!");
                Console.WriteLine("O mundo foi salvo graças à coragem de todos os heróis!");
            }
            else
            {
                Console.WriteLine("\nTodos foram derrotados... O mundo mergulhou em trevas...");
            }

            Console.WriteLine("Obrigado por jogar!");
        }

        public static void MostrarStatus()
        {
            Console.WriteLine("\n=== STATUS DOS PERSONAGENS ===");
            foreach (var p in grupo)
            {
                Console.WriteLine($"{p.Nome}: Vida = {p.Vida}, Vidas Extras = {p.VidasExtras}, Poções = {p.Pocoes}, Moedas = {p.Moedas}, Ataque = {p.Ataque}");
            }
            Console.WriteLine($"Ouro total: {ouro}");
        }

        public static void Pausa()
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }
}