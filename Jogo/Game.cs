using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    // Lógica do jogo
    class Game
    {
        // Método para centralizar o texto
        public static void CentralizarTexto(string texto)
        {
            // Divide o texto em linhas
            string[] linhas = texto.Split('\n');

            foreach (string linha in linhas)
            {
                int espacos = (Console.WindowWidth - linha.Length) / 2;
                Console.WriteLine(new string(' ', Math.Max(0, espacos)) + linha);
            }
        }

        // Lista de personagens no grupo do jogador
        public static List<Personagem> grupo = new List<Personagem>();

        // Quantidade total de ouro acumulado
        public static int ouro = 0;

        // Mundos que serão visitados durante a jornada
        public static string[] mundos = { "Floresta Sombria", "Deserto Ardente", "Montanhas Congeladas", "Ruínas Perdidas" };

        // Menu inicial onde o jogador escolhe sexo e nome do protagonista
        public static void MenuPrincipal()
        {
            // Introdução épica
            Console.Beep(200, 500);
            Console.Beep(150, 500);
            Console.ForegroundColor = ConsoleColor.Blue; // Muda a cor da fonte
            CentralizarTexto(" Bem-vindo ao Jogo Dimensões Quebradas! {Emojis.Estrela}");
            Console.ResetColor(); // Tem que fechar com o Reset para não continuar a cor em tudo
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nEscolha o sexo do personagem principal (1 - Mulher, 2 - Homem): ");
            string escolha = Console.ReadLine();
            Console.Beep(600, 100); // Som de quando o jogador faz uma escolha válida
            Console.Write("\nDigite o nome do seu personagem principal: ");
            string nomePrincipal = Console.ReadLine();
            Console.ResetColor();

            // Monta o grupo com base no sexo escolhido
            if (escolha == "1") // Jogadora mulher
            {
                grupo.Add(new Personagem(nomePrincipal, "Espada"));
                grupo.Add(new Personagem("Zarek", "Arco e Flecha"));
                grupo.Add(new Personagem("Nira", "Livro de Magias"));
                grupo.Add(new Personagem("Robô", "Raio Laser"));
            }
            else // Jogador homem
            {
                grupo.Add(new Personagem(nomePrincipal, "Espada"));
                grupo.Add(new Personagem("Nira", "Arco e Flecha"));
                grupo.Add(new Personagem("Zarek", "Livro de Magias"));
                grupo.Add(new Personagem("Robô", "Raio Laser"));
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            CentralizarTexto("\nA aventura começou!");
            Console.ResetColor();
            MostrarStatus(); // Exibe status dos personagens
            Pausa();         // Pausa para o jogador ler
            IniciarMundos(); // Começa os mundos do jogo
        }

        // Percorre todos os mundos, enfrentando inimigos e chefes
        public static void IniciarMundos()
        {
            for (int i = 0; i < mundos.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                CentralizarTexto($"\n=== Mundo: {mundos[i]} ===");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Herói principal aqui: {grupo[i].Nome} com sua arma: {grupo[i].Arma}");
                Pausa();
                Console.ResetColor();

                // Gera os inimigos do mundo atual
                List<Inimigo> inimigos = GerarInimigos(mundos[i]);

                // Combate contra cada inimigo comum
                foreach (var inimigo in inimigos)
                {
                    Combate(grupo[i], inimigo);
                    MostrarStatus();
                    Pausa();
                }

                // Define o chefe (boss) de cada mundo
                Inimigo boss = mundos[i] switch
                {
                    "Floresta Sombria" => new Inimigo("Guardião da Árvore Negra", 50, 12),
                    "Deserto Ardente" => new Inimigo("Faraó das Cinzas", 60, 14),
                    "Montanhas Congeladas" => new Inimigo("Rainha da Tempestade", 70, 16),
                    "Ruínas Perdidas" => new Inimigo("Guardião do Tempo Esquecido", 80, 18),
                    _ => new Inimigo($"Chefe de {mundos[i]}", 60, 15)
                };

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nChefe apareceu: {boss.Nome}!");
                Console.ResetColor();
                Pausa();
                Combate(grupo[i], boss); // Luta contra o chefe
                MostrarStatus();
                Pausa();
                Loja(); // Loja após o mundo
            }

            // Após todos os mundos, enfrenta o boss final
            MundoFinal();
        }

        // Gera lista de inimigos com base no mundo atual
        public static List<Inimigo> GerarInimigos(string mundo)
        {
            List<Inimigo> inimigos = new List<Inimigo>();

            // Inimigos específicos de cada mundo
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

        // Sistema de combate entre um herói e um inimigo
        public static void Combate(Personagem heroi, Inimigo inimigo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{heroi.Nome} encontrou {inimigo.Nome}!");
            Pausa();
            Console.ResetColor();

            // Enquanto os dois estiverem vivos
            while (inimigo.Vida > 0 && (heroi.Vida > 0 || heroi.VidasExtras > 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nVida de {heroi.Nome}: {heroi.Vida} | Vida do Inimigo: {inimigo.Vida}");
                Console.WriteLine("Escolha uma ação: 1-Atacar 2-Defender 3-Usar Poção");
                string acao = Console.ReadLine();
                Console.ResetColor();

                switch (acao)
                {
                    case "1":
                        Console.Beep(1200, 100); // Som agudo de ataque
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{heroi.Nome} atacou com {heroi.Arma}!");
                        inimigo.Vida -= heroi.Ataque;
                        Console.ResetColor();
                        break;
                    case "2":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{heroi.Nome} defendeu!");
                        Console.ResetColor();
                        continue;
                    case "3":
                        if (heroi.Pocoes > 0)
                        {
                            heroi.Vida = Math.Min(100, heroi.Vida + 50); // Cura até no máx. 100
                            heroi.Pocoes--;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{heroi.Nome} usou uma poção! Vida agora: {heroi.Vida}");
                            Console.ResetColor();
                        }
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sem poções!");
                        Console.ResetColor();
                        continue;
                }

                // Inimigo contra-ataca se ainda estiver vivo
                if (inimigo.Vida > 0)
                {
                    Console.Beep(200, 200); // Som grave de dano
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{inimigo.Nome} atacou {heroi.Nome}!");
                    heroi.Vida -= inimigo.Dano;
                    Console.ResetColor();

                    // Se vida chegou a 0 e ainda tem vidas extras
                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0)
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = 100;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{heroi.Nome} perdeu uma vida extra!");
                        Console.ResetColor();
                    }
                }

                Pausa();
            }

            // Recompensa ao vencer o inimigo
            if (inimigo.Vida <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{inimigo.Nome} derrotado!");
                ouro += 20;
                heroi.Moedas += 20;
                Console.ResetColor();
            }
        }

        // Loja para comprar poções ou melhorar arma
        public static void Loja()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            CentralizarTexto("\nBem-vindo à loja!");

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
                            else Console.WriteLine("Moedas insuficientes!");
                            break;

                        case "2":
                            if (heroi.Moedas >= 15)
                            {
                                heroi.Ataque += 5;
                                heroi.Moedas -= 15;
                                Console.WriteLine($"{heroi.Nome} melhorou sua Arma!");
                            }
                            else Console.WriteLine("Moedas insuficientes!");
                            break;

                        case "3":
                            continuarComprando = false;
                            break;

                        default:
                            Console.Beep(300, 800); // Som longo de aviso
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            }

            Console.ResetColor();
        }

        // Boss final com todos os heróis juntos
        public static void MundoFinal()
        {
            // Introdução épica
            Console.Beep(200, 500);
            Console.Beep(150, 500);
            Console.ForegroundColor = ConsoleColor.Blue;
            CentralizarTexto("\n--- MUNDO FINAL ---");
            Console.WriteLine("Todos os heróis se unem contra o Boss Final: Mestre da Ilusão!");
            Pausa();
            Console.ResetColor();

            Inimigo bossFinal = new Inimigo("Mestre da Ilusão", 200, 25);

            // Loop de batalha com todos os personagens vivos
            while (bossFinal.Vida > 0 && grupo.Exists(p => p.Vida > 0 || p.VidasExtras > 0))
            {
                foreach (var heroi in grupo)
                {
                    if (heroi.Vida <= 0 && heroi.VidasExtras == 0) continue;
                    if (bossFinal.Vida <= 0) break;

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nTurno de {heroi.Nome}:");
                    Console.WriteLine("1-Atacar 2-Defender 3-Usar Poção");
                    string acao = Console.ReadLine();
                    Console.ResetColor();

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
                                Console.WriteLine($"{heroi.Nome} usou uma poção! Vida agora: {heroi.Vida}");
                            }
                            else
                                Console.Beep(300, 800); // Som longo de aviso
                            Console.WriteLine("Sem poções!");
                            continue;
                    }

                    // O boss também ataca depois de cada turno
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Mestre da Ilusão atacou {heroi.Nome}!");
                    heroi.Vida -= bossFinal.Dano;
                    Console.ResetColor();

                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0)
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = 100;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"{heroi.Nome} perdeu uma vida extra!");
                        Console.ResetColor();
                    }

                    Pausa();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            CentralizarTexto(bossFinal.Vida <= 0 ? "\nPARABÉNS você venceu o jogo!!!" : "\nO grupo foi DERROTADO...");
            Console.ResetColor();
        }

        // Mostra o status de todos os personagens
        // Dentro da classe Game, modifique o método MostrarStatus:

        public static void MostrarStatus()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            CentralizarTexto("=== Status dos Personagens ===");

            foreach (var p in grupo)
            {


                Console.ForegroundColor = ConsoleColor.Yellow;
                CentralizarTexto($"» {p.Nome} «");

                // Barra de vida
                Console.ForegroundColor = ConsoleColor.Red;
                int vidaBarras = (int)Math.Ceiling(p.Vida / 10.0);
                string barraVida = new string('♥', vidaBarras).PadRight(10, ' ');
                CentralizarTexto($"Vida: [{barraVida}] {p.Vida}%");

                // Inventário visual
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                string inventario = $@"
        ╔════════════════╗
        ║   INVENTÁRIO   ║
        ╠════╦═══════════╣
        ║ ⚔️ ║ {p.Arma.PadRight(10)} ║
        ║ 🧪 ║ Poções: {p.Pocoes.ToString().PadRight(3)} ║
        ║ 🪙 ║ Moedas: {p.Moedas.ToString().PadRight(3)} ║
        ║ ✨ ║ Vidas: {p.VidasExtras.ToString().PadRight(3)} ║
        ╚════╩═══════════╝";

                // Centraliza cada linha do inventário
                foreach (var linha in inventario.Split('\n'))
                {
                    CentralizarTexto(linha);
                }

                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        // Pausa a execução até que o jogador pressione Enter
        public static void Pausa()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.ResetColor();
        }
    }
}
