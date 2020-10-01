using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Creditos : Escena
    {
        private RectangleShape fondo;
        private Text txtGracias;
        public Creditos()
        {
      
            Console.WriteLine("Hola soy el constructor de Creditos");
            Inicio();
            CargarContenido();
        }
        private void CargarContenido()
        {
            fondo = new RectangleShape(new Vector2f(600f, 600f))
            {
                FillColor = Color.Black,
            };
            txtGracias = new Text(" Gracias al Prof.Lucas Malvaso, a\nJuan M.J.Juairi mi compañero de cursada\n por soportarme fundirles\n la cabeza a preguntas\n" +
                "Alumno Max Sebastian Saldaña.\n\n\"M\" para volver al menu", new Font("Fuentes/fichin.ttf"));
            txtGracias.Scale = new Vector2f(0.7f, 0.7f);
            Console.WriteLine("Se carga Contenido");
        }
        public override void Inicio()
        {
            Console.WriteLine("Se carga Inicio");
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.M))
            {
                GerenteDeEscena.CargarEscena(new Menu());
            }
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
            ventana.Draw(txtGracias);
        }
        public override void Destruir()
        {
            Console.WriteLine("Hola soy el destructor de Creditos");
        }
    }
}
