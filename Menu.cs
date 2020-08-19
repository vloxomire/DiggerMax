using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Menu : Escena
    {
        Sprite fondo;
        Sprite raton;
        public override void Inicio()
        {
            fondo = new Sprite(new Texture("Sprites/montania.png"));
            fondo.Scale /= 2.3f;
            raton = new Sprite(new Texture(new Texture("Cursor/cursor.png")));
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
        }
        public override void Destruir()
        {
         
        }
    }
}
