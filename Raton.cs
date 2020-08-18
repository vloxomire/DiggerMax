using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace DiggerMax
{
    class Raton
    {
        Vector2i ratonPosicion;
        RenderWindow ventana;
        Sprite raton;
        public Raton(RenderWindow win) 
        {
            ventana = win;
            raton = new Sprite(new Texture(new Texture("Cursor/cursor.png")));
            raton.Scale /= 12f;
        }
        public void Actualizar() 
        {
            ratonPosicion = Mouse.GetPosition(ventana);
            raton.Position = new Vector2f(ratonPosicion.X, ratonPosicion.Y);
        }
        //private v
    }
}
