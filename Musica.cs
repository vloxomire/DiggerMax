using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;

namespace DiggerMax
{
    class Musica
    {
        public static Musica musicaInstancia;
        private Music musicMenu, musicGamePlay;
        public Musica() 
        {
            musicaInstancia = this;
            Inicializador();
        }
        private void Inicializador() 
        {
            //Console.Clear();
            try
            {
                musicMenu = new Music("Recursos/codex2015.ogg");
                musicGamePlay = new Music("Recursos/doom.ogg");
                Console.WriteLine("Se cargan los temas musicales");
            }
            catch (Exception)
            {
                Console.WriteLine("No se pudo carga la musica"); 
            }
        }
        public void GestorMusica(int pista)
        {
            switch (pista)
            {
                case 1:
                    musicMenu.Play();
                    break;
                case 2:
                    musicGamePlay.Play();
                    break;
                default:
                    break;
            }  
        }
        public void DetenerSonido() 
        {
            musicMenu.Stop();
        }
    }
}
