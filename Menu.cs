using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace DiggerMax
{
    class Menu : Escena
    {
        Sprite fondo,boton1;
        public override void Inicio()
        {
            fondo = new Sprite(new Texture("sprite/montania.png"));
            fondo.Scale /= 2.3f;

            boton1 = new Sprite(new Texture("sprite/nuevoJuego2.png"));
            boton1.Origin = new Vector2f(boton1.GetGlobalBounds().Width / 2.0f, boton1.GetGlobalBounds().Height / 2.0f);
            boton1.Position = new Vector2f(Juego.width/2.0f,200.0f);
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (boton1.GetGlobalBounds().Contains(posicionRaton.X,posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new ComoSeJuega());
                }
            }
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
            ventana.Draw(boton1);
        }
        public override void Destruir()
        {
         
        }
    }
}
