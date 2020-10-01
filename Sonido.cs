using System;
using System.Threading;
using SFML.Audio;

namespace DiggerMax
{
    class Sonido
    {
        public static Sonido sonidoInstancia;
        private SoundBuffer soundBuffer;
        private Sound soundPortal;
        public Sonido() 
        {
            sonidoInstancia = this;
            soundBuffer=new SoundBuffer("Recursos/portalPhaseJump.wav");
            Inicializador();
        }
        private void Inicializador() 
        {
            Console.Clear();
            try
            {
                soundPortal =new Sound(soundBuffer);
                Console.WriteLine("Se cargan el sonido");
            }
            catch (Exception)
            {
                Console.WriteLine("No se pudo carga el sonido");
            }
        }
        public void PlayPortalSound() 
        {
            soundPortal.Play();
        }
    }
}
