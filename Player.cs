using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace DiggerMax
{
    class Player : GameObject
    {
        float velX = 0.01f;
        float velY = 0.01f;
        Keyboard.Key teclaIzq;
        Keyboard.Key teclaDer;
        public Player(Texture text,Keyboard.Key izquierda,Keyboard.Key derecha,float x, float y):base(text,x,y)
        {
            teclaIzq = izquierda;
            teclaDer = derecha;
            renderer.Origin = new Vector2f(50.0f,25.0f);
        }
        public override void Actualizar(float DeltaTime)
        {
            //Moverse a la izquierda
            if (Keyboard.IsKeyPressed(teclaIzq))
            {
                if (renderer.Position.X >=0.0f + renderer.GetGlobalBounds().Width /2)
                {
                    renderer.Position += new Vector2f(-velX * DeltaTime, 0.0f);
                }
            }
            //Moverse a la derecha
            if (Keyboard.IsKeyPressed(teclaDer)) 
            {
                if (renderer.Position.X <= Juego.width - renderer.GetGlobalBounds().Width/2)
                {
                    renderer.Position += new Vector2f(velX * DeltaTime,0.0f);
                }
            }
        }
    }
}
