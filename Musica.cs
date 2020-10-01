using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;

namespace DiggerMax
{
    class Musica
    {
        public static Musica musicaInstancia;
        private SoundBuffer sonidoBuffer;
        private Sound sonidoMenu, sonidoGamePlay;
        public Musica() 
        {
            musicaInstancia = this;
            Inicializador();
        }
        private void Inicializador() 
        {
            Console.Clear();
            try
            {
                sonidoBuffer = new SoundBuffer("Recursos/codex2015.ogg");
                sonidoMenu = new Sound(sonidoBuffer);
                sonidoGamePlay = new Sound(new SoundBuffer("Recursos/doom.ogg"));
                Console.WriteLine("Se carga el buffer y el tema musical");
            }
            catch (Exception)
            {

                Console.WriteLine("No se pudo carga el buffer y el tema musical"); 
            }
        }
        public void GestorSonido(int pista)
        {
            switch (pista)
            {
                case 1:
                    sonidoMenu.Play();
                    break;
                case 2:
                    sonidoGamePlay.Play();
                    break;
                default:
                    break;
            }
            
            
        }
        public void DetenerSonido() 
        {
            sonidoMenu.Stop();
        }
    }
}
