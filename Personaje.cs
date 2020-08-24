using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace DiggerMax
{
    class Personaje : GameObject
    {
        float velX = 100f;
        float velY = 100f;
        Keyboard.Key izquierda;
        Keyboard.Key derecha;
        Keyboard.Key subir;
        Keyboard.Key bajar;
        public Personaje(Texture texture,Keyboard.Key izquierda,Keyboard.Key derecha,Keyboard.Key subir,Keyboard.Key bajar,float x, float y):base(texture,x,y)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.subir = subir;
            this.bajar = bajar;
            //Punto del origen del pj
            renderer.Origin = new Vector2f(renderer.GetLocalBounds().Width/2,renderer.GetLocalBounds().Height/2);
        }
        public override void Actualizar(float DeltaTime)
        {
            //Moverse a la izquierda
            if (Keyboard.IsKeyPressed(izquierda))
            {
                if (renderer.Position.X >=0.0f + renderer.GetGlobalBounds().Width /2)
                {
                    renderer.Position += new Vector2f(-velX * DeltaTime, 0.0f);
                }
            }
            //Moverse a la derecha
            if (Keyboard.IsKeyPressed(derecha)) 
            {
                if (renderer.Position.X <= Juego.width - renderer.GetGlobalBounds().Width/2)
                {
                    renderer.Position += new Vector2f(velX * DeltaTime,0.0f);
                }
            }
            //Subir
            if (Keyboard.IsKeyPressed(subir))
            {
                if (renderer.Position.Y >=0.0f + renderer.GetGlobalBounds().Height/2)
                {
                    renderer.Position += new Vector2f(0.0f,-velY * DeltaTime);
                }
            }
            //Bajar
            if (Keyboard.IsKeyPressed(bajar)) 
            {
                if (renderer.Position.Y <= Juego.height - renderer.GetGlobalBounds().Height / 2)
                {
                    renderer.Position += new Vector2f(0.0f,velY * DeltaTime);
                }
            }
        }
    }
}
