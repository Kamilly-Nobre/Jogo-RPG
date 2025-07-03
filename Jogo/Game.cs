using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    class Game
    {
        public static int ouro = 0;
        //PERSONAGENS SECUNDARIOS 
        public static List<Personagem> personagensSecundarios = new List<Personagem>
        {
            new Personagem("Zarek", "Arco e Flecha"),
            new Personagem("Nira", "Livro de Magias"),
            new Personagem("Robô", "Raio Laser"),
        };

        public static List<Personagem> grupo = new List<Personagem>(); //PERSONAGENS IRÃO SER ADICIONADOS AO DECORRER DO JOGO

        static class RNG //MÉTODO PARA RANDOMIZAR
        {
            public static readonly Random Shared = new Random();
        }
        public static void DistribuirRecompensa() //RANDOMIZAR MOEDAS DE ACORDO COM OS MUNDOS
        {
            int min = 10 + mundoAtual * 5;
            int max = 15 + mundoAtual * 10;
            int quantia = RNG.Shared.Next(min, max + 1); //QUANDO PASSA P PRÓXIMO MUNDO A QUANTIDADE DE MOEDAS AUMENTA

            foreach (var membro in grupo)
                membro.Moedas += quantia;

            ouro += quantia * grupo.Count; // ATUALIZA O VALOR DO OURO

            string recompensa = $@"
            ╔════════════════════════════════════╗
            ║ 💰 Cada membro recebeu {quantia} moedas!          ║
            ║ 🪙 Ouro total acumulado: {ouro}                ║
            ╚════════════════════════════════════╝";

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var linha in recompensa.Split('\n'))
                Textos.CentralizarTexto(linha.TrimEnd());
            Console.ResetColor();
        }

        public static bool AcertouAtaque(int chanceAcerto = 90) //CHANCE DE ACERTAR O ATAQUE
        {
            return RNG.Shared.Next(100) < chanceAcerto;
        }

        // MUNDOS
        public static string[] mundos = { "Floresta Sombria", "Deserto Ardente", "Montanhas Congeladas", "Ruínas Perdidas" };
        public static int mundoAtual = 1;

        public static void MenuPrincipal()
        {
            Console.Beep(200, 500);
            Console.Beep(150, 500);
            Console.ForegroundColor = ConsoleColor.Blue;

            Textos.CentralizarTexto(" Bem-vindo ao Jogo Dimensões Quebradas! {Emojis.Estrela}");
            Console.ResetColor();

            string escolha;
            do //LOOP PARA ESCOLHA DO SEXO
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nEscolha o sexo do personagem principal (1 - Mulher, 2 - Homem): ");
                escolha = Console.ReadLine();
                if (escolha != "1" && escolha != "2")
                {
                    Console.Beep(300, 500);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Textos.CentralizarTexto("❌ Opção inválida!");
                    Console.ResetColor();
                }
            } while (escolha != "1" && escolha != "2");
            Console.ResetColor();

            string nomePrincipal;
            do //LOOP PARA ESCOLHA DO NOME
            {
                Console.Beep(600, 100);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("\nDigite o nome do seu personagem principal: ");
                nomePrincipal = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomePrincipal)) //SE ESTIVER NULO DÁ ERRO
                {
                    Console.Beep(300, 500);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Textos.CentralizarTexto("❌ Digite um nome válido");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(nomePrincipal));
            Console.ResetColor();
            Console.Clear();

            grupo.Clear();
            grupo.Add(new Personagem(nomePrincipal, "Espada"));

            Textos.TextoInicial();

            Textos.MostrarStatusGrupo(grupo);
            Textos.Pausa();

            Console.Clear();
            IniciarMundos(escolha);
        }
        public static void IniciarMundos(string escolha) // LÓGICA DOS MUNDOS
        {
            for (int i = 0; i < mundos.Length; i++)
            {
                mundoAtual = i + 1;

                if (i > 0)
                {

                    int index = i - 1;
                    if (index < personagensSecundarios.Count) // ADICIONA OS PERSONAGENS DEPENDENDO DO SEXO
                    {
                        var ordem = escolha == "1"
                            ? new List<string> { "Zarek", "Nira", "Robô" } // SEXO FEMININO
                            : new List<string> { "Nira", "Zarek", "Robô" }; // SEXO MASCULINO

                        string nomePersonagem = ordem[index];
                        var personagem = personagensSecundarios.First(p => p.Nome == nomePersonagem);
                        grupo.Add(personagem);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Textos.CentralizarTexto($"\n{personagem.Nome} entrou para o grupo!");
                        Console.ResetColor();
                        Textos.Pausa();
                        Console.Clear();
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Textos.CentralizarTexto($"\n=== Mundo: {mundos[i]} ===");
                Console.ResetColor();

                Textos.MostrarStatusGrupo(grupo);

                //GERAR INIMIGOS
                List<Inimigo> inimigos = GerarInimigos(mundos[i]);

                Console.Clear();
                foreach (var inimigo in inimigos)
                {
                    if (i == 0)
                    {
                        Combate(grupo[0], inimigo);
                    }
                    else
                    {
                        CombateGrupo(grupo, inimigo);
                    }
                    Textos.MostrarStatusGrupo(grupo);
                }

                //CHEFÕES
                Inimigo boss = mundos[i] switch
                {
                    "Floresta Sombria" => new Inimigo("Eco dos Mortos", 50, 18, 25),
                    "Deserto Ardente" => new Inimigo("Faraó das Cinzas", 70, 27, 35),
                    "Montanhas Congeladas" => new Inimigo("Rainha da Tempestade", 90, 32, 45),
                    "Ruínas Perdidas" => new Inimigo("Guardião do Tempo Esquecido", 130, 40, 55)
                };

                Console.Clear();

                Textos.MostrarStatusGrupo(grupo);
                Textos.Pausa();
                Console.Clear();

                if (i == 0)
                {
                    Combate(grupo[0], boss);
                }
                else
                {
                    CombateGrupo(grupo, boss);
                }

                Textos.MostrarStatusGrupo(grupo);
                Textos.Pausa();
                Loja();
            }
            MundoFinal();
        }
        public static List<Inimigo> GerarInimigos(string mundo)   //GERA LISTA DE INIMIGOS
        {
            List<Inimigo> inimigos = new List<Inimigo>();

            switch (mundo)
            {
                case "Floresta Sombria":
                    inimigos.Add(new Inimigo("Espreitador das Sombras", 35, 8, 13));
                    inimigos.Add(new Inimigo("Podridão Andante", 40, 10, 14));
                    break;

                case "Deserto Ardente":
                    inimigos.Add(new Inimigo("Escorpião de Fogo", 50, 16, 20));
                    inimigos.Add(new Inimigo("Ladrão das Dunas", 55, 17, 22));
                    inimigos.Add(new Inimigo("Serpente Solar", 65, 20, 25));
                    break;

                case "Montanhas Congeladas":
                    inimigos.Add(new Inimigo("Golem de Gelo", 80, 25, 30));
                    inimigos.Add(new Inimigo("Yeti Selvagem", 100, 27, 33));
                    inimigos.Add(new Inimigo("Nevasca Viva", 115, 30, 35));
                    break;

                case "Ruínas Perdidas":
                    inimigos.Add(new Inimigo("Espectro Antigo", 130, 35, 40));
                    inimigos.Add(new Inimigo("Sentinela de Pedra", 150, 37, 43));
                    inimigos.Add(new Inimigo("Múmia Desperta", 170, 40, 45));
                    break;
            }
            return inimigos;
        }
        public static void Combate(Personagem heroi, Inimigo inimigo) //COMBATE SOLO
        {
            string[] chefesDosMundos = { "Eco dos Mortos", "Faraó das Cinzas", "Rainha da Tempestade", "Guardião do Tempo Esquecido" };
            bool inimigoBoss = chefesDosMundos.Contains(inimigo.Nome);

            if (inimigoBoss)
            {
                Textos.TextoBoss(inimigo.Nome);
                Textos.Pausa();
            }
            else
            {
                Textos.TextoInimigo(heroi.Nome, inimigo.Nome);
                Textos.Pausa();
            }
            Console.Clear();
            Console.ResetColor();

            while (inimigo.Vida > 0 && (heroi.Vida > 0 || heroi.VidasExtras > 0))
            {
                Console.Clear();
                Textos.MostrarStatus(heroi, inimigo);
                Textos.MenuAcoes();

                string acao = Console.ReadLine();
                Console.Clear();
                Console.ResetColor();

                switch (acao)
                {
                    case "1": // ATAQUE
                        Console.Beep(1200, 100);
                        Console.ForegroundColor = ConsoleColor.Red;

                        if (AcertouAtaque())
                        {
                            inimigo.Vida -= heroi.Ataque;
                            Textos.TextoAtaqueHeroi(heroi.Nome, inimigo.Nome, heroi.Ataque);
                        }
                        else
                        {
                            Textos.TextoErroAtaque(heroi.Nome);
                        }
                        Console.ResetColor();
                        Textos.Pausa();
                        break;

                    case "2": // DEFESA

                        Textos.TextoDefesa(heroi.Nome);
                        continue;

                    case "3": // USAR POÇÃO
                        Console.Clear();
                        if (heroi.Pocoes > 0)
                        {
                            heroi.Vida = Math.Min(heroi.VidaMaxima, heroi.Vida + 50);
                            heroi.Pocoes--;
                            Textos.TextoUsoPocao(heroi.Nome, heroi.Vida);
                        }
                        else
                        {
                            Textos.TextoSemPocao(heroi.Nome);

                        }
                        Console.ResetColor();
                        Textos.Pausa();
                        break;

                    default:
                        Console.Clear();
                        Textos.TextoOpcaoInvalida();

                        Console.ResetColor();
                        Textos.Pausa();
                        continue;
                }

                if (inimigo.Vida > 0)
                {
                    Console.Beep(200, 200);
                    Console.ForegroundColor = ConsoleColor.Red;
                    int dano = inimigo.GerarDano();

                    if (AcertouAtaque())
                    {
                        Console.Clear();
                        heroi.Vida -= dano;
                        Textos.TextoAtaqueInimigo(inimigo.Nome, heroi.Nome, dano);

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        Textos.TextoErroInimigo(inimigo.Nome);
                        Console.ResetColor();


                    }

                    // VIDAS EXTRAS
                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0)
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = heroi.VidaMaxima;
                        Console.Clear();
                        Textos.TextoPerdeuVidaExtra(heroi.Nome);
                        Console.ResetColor();
                    }
                }

                if (inimigo.Vida > 0)
                {
                    Textos.Pausa();
                    Console.Clear();
                }
            }

            if (inimigo.Vida <= 0) // DERROTA DO INIMIGO
            {
                Console.Clear();
                Textos.TextoDerrotaInimigo(inimigo.Nome);
                DistribuirRecompensa();
                Textos.Pausa();
                Console.Clear();

                if (RNG.Shared.Next(100) < 30)
                {
                    heroi.Pocoes++;
                    Textos.TextoEncontrouPocao(heroi.Nome);
                }

                if (chefesDosMundos.Contains(inimigo.Nome))
                {
                    if (RNG.Shared.Next(100) < 5)
                    {
                        heroi.FragmentosDaAlma++;
                        Textos.TextoFragmentoAlma(heroi.Nome);
                    }
                }
                Console.ResetColor();
                Console.Clear();
            }

        }
        public static void CombateGrupo(List<Personagem> grupo, Inimigo inimigo) //COMBATE EM GRUPO
        {
            string[] chefesDosMundos = { "Eco dos Mortos", "Faraó das Cinzas", "Rainha da Tempestade", "Guardião do Tempo Esquecido" };
            bool inimigoBoss = chefesDosMundos.Contains(inimigo.Nome);

            if (inimigoBoss)
            {
                Textos.TextoBoss(inimigo.Nome);
                Textos.Pausa();
                Console.Clear();
            }
            else
            {
                Textos.TextoInimigo(grupo[0].Nome, inimigo.Nome);
                Textos.Pausa();
                Console.Clear();
            }

            Console.ResetColor();

            while (inimigo.Vida > 0 && grupo.Any(p => p.Vida > 0 || p.VidasExtras > 0))
            {
                foreach (var heroi in grupo)
                {
                    if (heroi.Vida <= 0 && heroi.VidasExtras == 0) continue;

                    Console.Clear();
                    Textos.MostrarStatus(heroi, inimigo);
                    Textos.MenuAcoes();

                    bool defendeu = false;

                    string acao = Console.ReadLine();

                    Console.ResetColor();

                    switch (acao)
                    {
                        case "1": // ATAQUE
                            Console.Beep(1200, 100);
                            Console.ForegroundColor = ConsoleColor.Red;

                            if (AcertouAtaque())
                            {
                                inimigo.Vida -= heroi.Ataque;
                                Textos.TextoAtaqueHeroi(heroi.Nome, inimigo.Nome, heroi.Ataque);
                            }
                            else // ERRO DE ATAQUE DO HEROI
                            {
                                Textos.TextoErroAtaque(heroi.Nome);
                            }
                            Console.ResetColor();
                            Textos.Pausa();
                            break;

                        case "2": // DEFESA
                            defendeu = true;
                            Textos.TextoDefesa(heroi.Nome);
                            Textos.Pausa();
                            break;

                        case "3": // USAR POÇÃO
                            if (heroi.Pocoes > 0)
                            {
                                heroi.Vida = Math.Min(heroi.VidaMaxima, heroi.Vida + 50);
                                heroi.Pocoes--;
                                Textos.TextoUsoPocao(heroi.Nome, heroi.Vida);
                            }
                            else // SEM POÇÃO
                            {
                                Textos.TextoSemPocao(heroi.Nome);
                            }
                            Textos.Pausa();
                            break;

                        default:
                            Textos.TextoOpcaoInvalida();
                            Textos.Pausa();
                            continue;
                    }

                    if (inimigo.Vida > 0)
                    {
                        // ALEATORIZA QUEM O INIMIGO VAI ATACAR
                        var alvosValidos = grupo.Where(p => p.Vida > 0).ToList();

                        if (alvosValidos.Count == 0) break;

                        var alvo = alvosValidos[RNG.Shared.Next(alvosValidos.Count)];

                        int dano = inimigo.GerarDano();

                        if (alvo == heroi && defendeu)
                        {
                            int chance = RNG.Shared.Next(100);
                            if (chance < 50)
                            {
                                dano = 0;
                                Textos.TextoDefesa(heroi.Nome);
                            }
                            else
                            {
                                dano /= 2;
                                Textos.TextoMetadeDefesa(heroi.Nome);
                            }
                            Console.ResetColor();
                        }

                        if (dano > 0)
                        {
                            alvo.Vida -= dano;
                            Textos.TextoAtaqueInimigo(inimigo.Nome, alvo.Nome, dano);
                        }
                        else
                        {
                            Textos.TextoErroInimigo(inimigo.Nome);
                        }
                        Console.ResetColor();
                        if (alvo.Vida <= 0)
                        {
                            if (alvo.VidasExtras > 0)
                            {
                                alvo.VidasExtras--;
                                alvo.Vida = alvo.VidaMaxima;
                                Textos.TextoPerdeuVidaExtra(alvo.Nome);
                            }
                            else
                            {
                                Textos.TextoDerrotado(alvo.Nome);
                            }
                        }
                        Textos.Pausa();
                    }

                    if (inimigo.Vida <= 0) // DERROTA DO INIMIGO
                    {
                        Textos.TextoDerrotaInimigo(inimigo.Nome);
                        Textos.Pausa();

                        DistribuirRecompensa();

                        bool inimigoEhChefe = chefesDosMundos.Contains(inimigo.Nome); // RANDOM DE RECOMPENSA PARA CHEFÕES DE MUNDO

                        foreach (var heroiRecompensa in grupo.Where(h => h.Vida > 0 || h.VidasExtras > 0))
                        {
                            if (RNG.Shared.Next(100) < 25) // 25% DE CHANCE PARA OBTER POÇÃO
                            {
                                heroiRecompensa.Pocoes++;
                                Textos.TextoEncontrouPocao(heroiRecompensa.Nome);
                            }

                            if (inimigoEhChefe && RNG.Shared.Next(100) < 10) // 10% DE CHANCE P OBTER FRAGMENTO
                            {
                                heroiRecompensa.FragmentosDaAlma++;
                                Textos.TextoFragmentoAlma(heroiRecompensa.Nome);
                            }
                        }
                        Console.ResetColor();
                        Textos.Pausa();
                        return;
                    }
                }
            }
            Textos.TextoVitoria(inimigo.Vida <= 0);
        }
        public static void Loja()
        {
            Console.Clear();

            foreach (var heroi in grupo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Textos.CentralizarTexto($"\nLoja para {heroi.Nome}!");

                bool continuarComprando = true;
                while (continuarComprando)
                {
                    string menuLoja = $@"
                    🛒 LOJA DO HERÓI
                    ──────────────────────────────
                    Moedas disponíveis: {heroi.Moedas}
                    1️⃣  Poção de Cura (Cura 50HP)........... 15 moedas
                    2️⃣  Elixir da Vida (Aumenta mais 50HP da sua vida)........ 60 moedas
                    3️⃣  Fragmento da Alma (Utilize para reviver um aliado)..... 100 moedas
                    4️⃣  Melhorar Arma ......... {heroi.CustoMelhoriaArma} moedas
                    5️⃣  Sair
                    ──────────────────────────────";

                    foreach (var linha in menuLoja.Split('\n'))
                        Textos.CentralizarTexto(linha);

                    string escolha = Console.ReadLine();
                    Console.Clear();

                    switch (escolha)
                    {
                        case "1": //POÇÃO DE CURA
                            if (heroi.Moedas >= 15)
                            {
                                heroi.Pocoes++;
                                heroi.Moedas -= 15;

                                Textos.TextoCompraPocao(heroi.Nome, heroi.Pocoes);
                            }
                            else
                            {
                                Textos.TextoErroMoedas();
                                Textos.Pausa();
                            }
                            break;

                        case "2": //ELIXIR DA VIDA
                            if (heroi.Moedas >= 60)
                            {
                                heroi.Moedas -= 60;
                                heroi.VidaMaxima += 50;
                                heroi.Vida = heroi.VidaMaxima;
                                Textos.TextoCompraElixir(heroi.Nome, heroi.VidaMaxima);
                            }
                            else
                            {
                                Textos.TextoErroMoedas();
                                Textos.Pausa();
                            }
                            break;

                        case "3": //FRAGMENTO DA ALMA
                            if (heroi.Moedas >= 100)
                            {
                                heroi.Moedas -= 100;
                                heroi.FragmentosDaAlma++;
                                Textos.TextoCompraFragmento(heroi.Nome, heroi.FragmentosDaAlma);
                            }
                            else
                            {
                                Textos.TextoErroMoedas();
                                Textos.Pausa();
                            }
                            break;

                        case "4": //MELHORAR ARMA
                            if (heroi.Ataque >= 160)
                            {
                                Textos.TextoArmaMaxima();
                            }
                            else if (heroi.Moedas >= heroi.CustoMelhoriaArma)
                            {

                                heroi.Moedas -= heroi.CustoMelhoriaArma;
                                heroi.Ataque *= 2;

                                if (heroi.Ataque > 160)
                                {
                                    heroi.Ataque = 160;
                                }

                                heroi.CustoMelhoriaArma += 50;
                                Textos.TextoMelhorouArma(heroi.Nome, heroi.Ataque);
                            }
                            else // SE NÃO HOUVER MOEDAS SUFICIENTE
                            {
                                Textos.TextoErroMoedas();
                                Textos.Pausa();
                            }
                            break;

                        case "5": //SAIR
                            continuarComprando = false;
                            Console.Clear();
                            break;

                        default: //OPÇÃO INVÁLIDA
                            Textos.TextoOpcaoInvalida();
                            Textos.Pausa();
                            break;
                    }
                }
            }
            Console.ResetColor();
        }

        public static void MundoFinal() //BOSS FINAL
        {
            Console.Beep(200, 500);
            Console.Beep(150, 500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Textos.CentralizarTexto("\n--- MUNDO OCULTO ---");
            Textos.TextoIntroducaoBossFinal();
            Console.ResetColor();

            Inimigo bossFinal = new Inimigo("Mestre da Ilusão", 500, 55, 65);

            while (bossFinal.Vida > 0 && grupo.Exists(p => p.Vida > 0 || p.VidasExtras > 0))
            {
                foreach (var heroi in grupo) //BATALHA EM GRUPO
                {

                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0) //CASO MORRA PERDE UMA VIDA EXTRA
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = heroi.VidaMaxima;
                        Textos.TextoPerdeuVidaExtra(heroi.Nome);
                    }

                    if (heroi.Vida <= 0 && heroi.VidasExtras == 0) continue;
                    if (bossFinal.Vida <= 0) break;

                    Textos.MenuAcoes();
                    string acao = Console.ReadLine();

                    //ATAQUE BOSS FINAL
                    int danoBoss = bossFinal.GerarDano();
                    heroi.Vida -= danoBoss;
                    Textos.TextoAtaqueBossFinal(heroi.Nome, danoBoss);
                    Console.ResetColor();

                    switch (acao)
                    {
                        case "1": //ATAQUE
                            bossFinal.Vida -= heroi.Ataque;
                            Textos.TextoAtaqueContraBoss(heroi.Nome, danoBoss);
                            break;
                        case "2": // DEFESA
                            Textos.TextoDefesa(heroi.Nome);
                            continue;
                        case "3": //USAR POÇÕES
                            if (heroi.Pocoes > 0)
                            {
                                heroi.Vida = Math.Min(100, heroi.Vida + 50);
                                heroi.Pocoes--;
                                Textos.TextoUsoPocao(heroi.Nome, heroi.Vida);
                            }
                            else // SE NÃO  HOUVER POÇÕES
                                Textos.TextoSemPocao(heroi.Nome);
                            continue;
                    }

                    if (heroi.Vida <= 0 && heroi.VidasExtras > 0)
                    {
                        heroi.VidasExtras--;
                        heroi.Vida = 100;
                        Textos.TextoPerdeuVidaExtra(heroi.Nome);
                    }
                    Textos.Pausa();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Textos.CentralizarTexto(bossFinal.Vida <= 0 ? "\nPARABÉNS você venceu o jogo!!!" : "\nO grupo foi DERROTADO...");
            Console.ResetColor();
        }
    }
}