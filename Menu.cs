using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Menu : Escena
    {
        Sprite raton;
        public override void Inicio()
        {
            raton = new Sprite(new Texture(new Texture("Cursor/cursor.png")));
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            
        }
        public override void Dibujar(RenderWindow ventana)
        {
            
        }
        public override void Destruir()
        {
         
        }
    }
}
