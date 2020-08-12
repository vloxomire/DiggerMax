using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Juego
    {
        private RenderWindow ventana;
        public Juego() 
        {
            var modo = new VideoMode(800,600);
            ventana = new RenderWindow(modo, "SFLM, funciona!!!");
            ventana.KeyPressed += Ventana_PresionarTecla;//Levanta la tecla presionada?
        }
        public void Run() 
        {
            var circulo = new CircleShape(100f)
            {
                FillColor = Color.Blue
            };
            Sonido.PlaySonido();
            //Console.Clear();
            //Sonido.PlayMusica();
            //LOOP DEL JUEGO
            while (ventana.IsOpen)
            {
                ventana.DispatchEvents();
                ventana.Draw(circulo);
                ventana.Display();
            }
        }
        private void Ventana_PresionarTecla(object sender,KeyEventArgs e) 
        {
            if (e.Code == Keyboard.Key.Escape) 
            {
                Window window = (Window)sender;
                window.Close();
            }
        }
    }
}
