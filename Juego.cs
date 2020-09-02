using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;

namespace DiggerMax
{
    class Juego
    {
        public static Juego instancia;

        private RenderWindow ventana;
        private Clock clock;
        private Sprite raton = null;
        private Personaje personaje;
        public static uint width = 600;
        public static uint height = 600;

        public Juego()
        {
            instancia = this;

            const string tituloVentana = "Digger";
            ventana = new RenderWindow(new VideoMode(width, height),tituloVentana);
            ventana.SetFramerateLimit(61);
            ventana.SetMouseCursorVisible(false);
            ventana.Closed += OnVentanaCerrada;
            ventana.KeyPressed += Ventana_PresionarTecla;//Levanta la tecla presionada?

            clock = new Clock();
            personaje = new Personaje();
            raton = new Sprite(new Texture("Sprite/pick.png"));
            raton.Position = new Vector2f(5.0f,0.0f);
            raton.Scale /= 20f;
        }
        public void Correr()
        {
            /*var circulo = new CircleShape(100f)
            {
                FillColor = Color.Blue
            };*/
            Sonido.PlaySonido();
            //GerenteDeEscena.CargarEscena(new Menu());
            GerenteDeEscena.CargarEscena(new ComoSeJuega());

            while (ventana.IsOpen)
            {
                Actualizar();
                Dibujar();
            }
        }
        private void Actualizar() 
        {
            Time tiempo = clock.Restart();
            float deltaTiempo = clock.Restart().AsSeconds();
            Vector2i ratonPosicion = Mouse.GetPosition(ventana);
            raton.Position = new Vector2f(ratonPosicion.X,ratonPosicion.Y);
            //raton.Actualizar();
            GerenteDeEscena.DameEscenaActual().Actualizar(tiempo.AsSeconds(),ratonPosicion);
            personaje.Actualizar(deltaTiempo);

            ventana.DispatchEvents();
        }
        private void Dibujar()
        {
            GerenteDeEscena.DameEscenaActual().Dibujar(ventana);
            ventana.Draw(raton);
            personaje.Dibujar(ventana);

            ventana.Display();
        }
        private void Ventana_PresionarTecla(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                Window window = (Window)sender;
                window.Close();
            }
        }
        private void OnVentanaCerrada(object sender,EventArgs e) 
        {
            Window win = (Window)sender;
            win.Close();
        }
        public Window DameVentana() 
        {
            return ventana;
        }
    }
}
