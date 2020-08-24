using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiggerMax
{
    class Casillero : GameObject
    {
        float x;
        float y;
        public Casillero(Texture terreno,float x, float y):base(terreno,x,y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Actualizar(float DeltaTime)
        {
            
        }
        public float GetPosicionX() 
        {
            return x;
        }
        public float GetPosicionY()
        {
            return y;
        }
        public void SetTexture(String textura) 
        {
            renderer = new Sprite(new Texture(textura));
        }
    }
}
