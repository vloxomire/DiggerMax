using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Casillero : GameObject
    {
        Sprite[,] casillas;
        public Casillero() 
        {
            casillas = new Sprite[5,5];
            casillas{ }
        }
        public override void Actualizar(float DeltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
