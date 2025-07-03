using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    internal class Textos
    {
        public static void CentralizarTexto(string texto)
        {
            string[] linhas = texto.Split('\n');

            foreach (string linha in linhas)
            {
                int espacos = (Console.WindowWidth - linha.Length) / 2;
                Console.WriteLine(new string(' ', Math.Max(0, espacos)) + linha);
            }
        }

        public static void TextoInicial()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            string textoInicial = @"
            Você acorda com um som estrondoso do lado de fora da sua casa.
            O barulho parece vir das profundezas da floresta ali próxima.

            Você levanta da sua cama — pronto para uma jornada inesperada.

            Ao se aproximar da origem do som, uma figura misteriosa surge à sua frente.
            Ele diz:

            'Você é o escolhido. Sua missão é reunir mais três guerreiros para lutarem ao seu lado contra o temível Guardião do Tempo Esquecido, 
            uma figura maligna que ameaça nosso mundo.'

            O estranho lhe entrega uma poção incomum, dizendo:
            'Se você se encontrar em uma situação de extremo perigo, beba esta poção para sobreviver.'

            O QUE SERÁ QUE LHE ESPERA?";

            Console.WriteLine(textoInicial);

            Console.ResetColor();
            Pausa();
        }


        public static void TextoInimigo(string nomeHeroi, string nomeInimigo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            CentralizarTexto($"\n⚠️ {nomeHeroi} encontrou {nomeInimigo}! ⚔️");
            Console.ResetColor();
        }

        public static void TextoBoss(string nomeChefe)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            CentralizarTexto($"\n⚔️ Chefão apareceu: {nomeChefe}! ⚔️");
            Console.ResetColor();
        }

        public static void MenuAcoes()
        {
            string menu = @"
            ╔══════════════════════════════╗
            ║     Escolha sua ação!       ║
            ╠══════════════════════════════╣
            ║  1 - ⚔️  Atacar               ║
            ║  2 - 🛡️  Defender             ║
            ║  3 - 🧪  Usar Poção           ║
            ╚══════════════════════════════╝";

            foreach (var linha in menu.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoAtaqueHeroi(string nomeHeroi, string nomeInimigo, int dano)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ 🔥 {nomeHeroi} atacou!     ║
            ║   Deu -{dano} de dano  ║
            ║       em {nomeInimigo}     ║
            ╚════════════════════════╝";

            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoErroAtaque(string nomeHeroi)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ ❌ {nomeHeroi} errou o ataque! ║
            ╚════════════════════════╝";

            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoDefesa(string nomeHeroi)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ 🛡️  {nomeHeroi} defendeu!     ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoUsoPocao(string nomeHeroi, int vidaAtual)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ 🧪 {nomeHeroi} usou uma poção! ║
            ║   Vida agora: {vidaAtual}         ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoSemPocao(string nomeHeroi)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ ⚠️  {nomeHeroi} não tem poções!  ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoOpcaoInvalida()
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ ❌ Opção inválida!       ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoAtaqueInimigo(string nomeInimigo, string nomeHeroi, int dano)
        {
            string msg = $@"
╔════════════════════════╗
║ 👾 {nomeInimigo} atacou!    ║
║   Deu -{dano} de dano         ║
║       em {nomeHeroi}       ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoErroInimigo(string nomeInimigo)
        {
            string msg = $@"
╔════════════════════════╗
║ ❌ {nomeInimigo} errou o ataque! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoPerdeuVidaExtra(string nomeHeroi)
        {
            string msg = $@"
╔════════════════════════╗
║ 💔 {nomeHeroi} foi ressucitado e perdeu uma vida extra! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoDerrotaInimigo(string nomeInimigo)
        {
            string msg = $@"
╔════════════════════════╗
║ 🏆 {nomeInimigo} foi derrotado! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoEncontrouPocao(string nomeHeroi)
        {
            string msg = $@"
╔════════════════════════╗
║ 🧪 {nomeHeroi} encontrou uma poção! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoFragmentoAlma(string nomeHeroi)
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ 👻 {nomeHeroi} encontrou um Fragmento da Alma! ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoMetadeDefesa(string nomeHeroi)
        {
            string msg = $@"
    ╔═══════════════════════════════════╗
    ║ 🛡️ {nomeHeroi} defendeu e sofreu metade do dano! ║
    ╚═══════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoDerrotado(string nomeHeroi)
        {
            string msg = $@"
    ╔══════════════════════════════════════╗
    ║ ☠️  {nomeHeroi} foi derrotado...      ║
    ╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoVitoria(bool vitoria)
        {
            string msg = vitoria ? "Vitória!" : "O grupo foi derrotado.";
            Console.ForegroundColor = ConsoleColor.Red;
            CentralizarTexto(msg);
            Console.ResetColor();
        }

        //TEXTOS LOJA
        public static void TextoCompraPocao(string nomeHeroi, int totalPocoes)
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ 🧪 {nomeHeroi} comprou uma Poção de Cura!       ║
║ Total de poções: {totalPocoes,3}                       ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoCompraElixir(string nomeHeroi, int novaVidaMax)
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ 💖 {nomeHeroi} usou o Elixir da Vida!          ║
║ Vida máxima agora é {novaVidaMax,3}            ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoCompraFragmento(string nomeHeroi, int totalFragmentos)
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ 👻 {nomeHeroi} comprou um Fragmento da Alma!    ║
║ Total: {totalFragmentos,3}                      ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoErroMoedas()
        {
            string msg = $@"
╔════════════════════════╗
║ 💰 Moedas insuficientes! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoArmaMaxima()
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ ⚔️  Sua arma já está no nível máximo!          ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void TextoMelhorouArma(string nomeHeroi, int novoAtaque)
        {
            string msg = $@"
╔══════════════════════════════════════╗
║ ⚔️  {nomeHeroi} melhorou sua arma!             ║
║ Ataque aumentou para: {novoAtaque,3}          ║
╚══════════════════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.Trim());
        }
        public static void TextoPersonalizado(string texto, int atraso = 40)
        {
            Console.Clear();
            string[] linhas = texto.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var linha in linhas)
            {
                int posicao = (Console.WindowWidth - linha.Length) / 2;
                if (posicao < 0) posicao = 0;
                Console.CursorLeft = posicao;

                foreach (char c in linha)
                {
                    Console.Write(c);
                    Thread.Sleep(atraso);
                }
                Console.WriteLine();
            }
        }
        public static void TextoIntroducaoBossFinal()
        {
            Console.Beep(200, 500);
            Console.Beep(150, 500);
            Console.ForegroundColor = ConsoleColor.Blue;
            CentralizarTexto("\n--- MUNDO OCULTO ---");

            string textoBossFinal = @"
            À medida que a névoa se dissipa, uma silhueta se forma...
            O tempo parece parar... 
            Você sente que esta é a batalha final.
            O Mestre da Ilusão está à sua frente, pronto para distorcer a realidade.";

            TextoPersonalizado(textoBossFinal, 40);
            Console.ResetColor();
        }

        public static void TextoAtaqueBossFinal(string nomeHeroi, int dano)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ 👾 Mestre da Ilusão atacou! ║
            ║   Deu -{dano} de dano         ║
            ║       em {nomeHeroi}         ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoErroAtaqueBoss(string nomeHeroi)
        {
            string msg = $@"
            ╔════════════════════════╗
            ║ ❌ Mestre da Ilusão errou o ataque! ║
            ╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoAtaqueContraBoss(string nomeHeroi, int dano)
        {
            string msg = $@"
╔════════════════════════╗
║ 🔥 {nomeHeroi} atacou!     ║
║   Deu -{dano} de dano  ║
║   em Mestre da Ilusão   ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void TextoErroAtaqueContraBoss(string nomeHeroi)
        {
            string msg = $@"
╔════════════════════════╗
║ ❌ {nomeHeroi} errou o ataque! ║
╚════════════════════════╝";
            foreach (var linha in msg.Split('\n'))
                CentralizarTexto(linha.TrimEnd());
        }

        public static void Pausa() //PAUSA PARA O JOGAR LER
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.ResetColor();
        }

        public static void MostrarStatus(Personagem heroi, Inimigo inimigo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string status = $@"
            ──── Status ────
            🧝 {heroi.Nome}
            ❤️ {heroi.Vida}/{heroi.VidaMaxima} HP

            VS

            👾 {inimigo.Nome}
            💢 {inimigo.Vida} HP
            ────────────────";

            foreach (var linha in status.Split('\n'))
                CentralizarTexto(linha.Trim());
        }

        public static void MostrarStatusGrupo(List<Personagem> grupo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            CentralizarTexto("=== Status dos Personagens ===");

            foreach (var p in grupo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                CentralizarTexto($"» {p.Nome} «");

                // BARRA DE VIDA
                Console.ForegroundColor = ConsoleColor.Red;
                int coracoesMaximos = p.VidaMaxima / 10;
                int coracoesCheios = Math.Min(p.Vida / 10, coracoesMaximos);
                int coracoesVazios = Math.Max(0, coracoesMaximos - coracoesCheios);

                string barraVida = new string('♥', coracoesCheios) + new string('♡', coracoesVazios);
                CentralizarTexto($"Vida: {barraVida} ({p.Vida}/{p.VidaMaxima})");

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                string inventario = $@"
        ╔════════════════╗
        ║   INVENTÁRIO   ║
        ╠════╦═══════════╣
        ║ ⚔️ ║ {p.Arma.PadRight(10)} ║
        ║ 🧪 ║ Poções: {p.Pocoes.ToString().PadRight(3)} ║
        ║ 🔮 ║ Fragmentos da Alma: {p.FragmentosDaAlma.ToString().PadRight(6)} ║
        ║ 🪙 ║ Moedas: {p.Moedas.ToString().PadRight(3)} ║
        ║ ✨ ║ Vidas: {p.VidasExtras.ToString().PadRight(3)} ║
        ╚════╩═══════════╝";

                foreach (var linha in inventario.Split('\n'))
                {
                    CentralizarTexto(linha.TrimEnd());
                }

                Console.ResetColor();
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}