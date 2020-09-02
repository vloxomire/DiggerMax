using System;
using System.Threading;
using SFML.Audio;

namespace DiggerMax
{
    static class Sonido
    {
        public static void PlaySonido()
        {
            //Cargo el sonido de tipo buffer de un archivo
            //var buffer = new SoundBuffer("sonido/canary.wav");
            //Creo una instancia de sonido y la ejecuto
            //var sound = new Sound(buffer);
            //Ejecuto
            //sound.Play();

            //Loop del sonido en el juego
            /* while (sound.Status == SoundStatus.Playing) 
             {
                 //Mostra la posicion
                 /*Console.CursorLeft = 0;
                 Console.WriteLine("Playing..."+sound.PlayingOffset+"sec ");

                 //Deja al cpu hacer otros procesos
                 Thread.Sleep(100);
             }*/
        }
        public static void PlayMusica()
        {
            //Cargamos la musica
            var musica = new Music("sonido/orchestral.ogg");
            musica.Play();//Ejecuto

            //Loop de la musica
            while (musica.Status == SoundStatus.Playing)
            {
                //Mostrame la posicion
                Console.CursorLeft = 0;
                Console.WriteLine("Playing..." + musica.PlayingOffset + "sec    ");

                //Dejar algo de tiempo al CPu para otrosd procesos
                Thread.Sleep(100);
            }
        }
    }
}
