using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Jogo
{
    class AudioManager
    {
        private static WaveOutEvent waveOut;
        private static AudioFileReader audioFile;

        public static void TocarMusica(string caminho)
        {
            // Para qualquer música anterior
            PararMusica();

            // Inicia nova música
            audioFile = new AudioFileReader(caminho);
            waveOut = new WaveOutEvent();

            // Configura loop infinito
            var loopStream = new LoopStream(audioFile);
            waveOut.Init(loopStream);
            waveOut.Play();
        }

        public static void PararMusica()
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            audioFile?.Dispose();
        }
    }

    // Classe adicional para loop infinito
    class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream source)
        {
            sourceStream = source;
        }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;
        public override long Length => sourceStream.Length;
        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

                if (bytesRead == 0)
                {
                    sourceStream.Position = 0;
                }

                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }
    }
}