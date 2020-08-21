using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Casillero : GameObject
    {
        Sprite[,] casillas;
        public Casillero(Texture tex):base(tex)
        {
            casillas = new Sprite[5,5];
            casillas[0,0]= new Sprite(new Texture("sprite/sheetTerreno.png"));
        }
        public override void Actualizar(float DeltaTime)
        {
            
        }
    }
}
