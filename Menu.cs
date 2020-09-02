using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace DiggerMax
{
    class Menu : Escena
    {
        Sprite fondo,bNuevo, bCargar, bCreditos, bSalir;
        public override void Inicio()
        {
            fondo = new Sprite(new Texture("Sprite/juego.png"));

            bNuevo = new Sprite(new Texture("sprite/boton1.png"));
            bNuevo.Origin = new Vector2f(bNuevo.GetGlobalBounds().Width / 2.0f, bNuevo.GetGlobalBounds().Height / 2.0f);
            bNuevo.Position = new Vector2f(30.0f,Juego.height/2.0f);

            bCargar = new Sprite(new Texture("sprite/boton2.png"));
            bCargar.Color = Color.Black;
            bCargar.Origin = new Vector2f(bCargar.GetGlobalBounds().Width / 2.0f, bCargar.GetGlobalBounds().Height / 2.0f);
            bCargar.Position = new Vector2f(200.0f, Juego.height / 2.0f);

            bCreditos = new Sprite(new Texture("sprite/boton3.png"));
            bCreditos.Color = Color.Blue;
            bCreditos.Origin = new Vector2f(bCreditos.GetGlobalBounds().Width / 2.0f, bCreditos.GetGlobalBounds().Height / 2.0f);
            bCreditos.Position = new Vector2f(400.0f, Juego.height / 2.0f);

            bSalir = new Sprite(new Texture("sprite/boton4.png"));
            bSalir.Color = Color.Red;
            bSalir.Origin = new Vector2f(bSalir.GetGlobalBounds().Width / 2.0f, bSalir.GetGlobalBounds().Height / 2.0f);
            bSalir.Position = new Vector2f(600.0f, Juego.height / 2.0f);
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (bNuevo.GetGlobalBounds().Contains(posicionRaton.X,posicionRaton.Y))
                {
                    GerenteDeEscena.CargarEscena(new ComoSeJuega());
                }
                if (bCargar.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y))
                {
                    
                }
                if (bCreditos.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y)) 
                {
                    GerenteDeEscena.CargarEscena(new Creditos());
                }
                if (bSalir.GetGlobalBounds().Contains(posicionRaton.X, posicionRaton.Y)) 
                {
                }
            }
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
            ventana.Draw(bNuevo);
            ventana.Draw(bCargar);
            ventana.Draw(bCreditos);
            ventana.Draw(bSalir);
        }
        public override void Destruir()
        {
         
        }
    }
}
