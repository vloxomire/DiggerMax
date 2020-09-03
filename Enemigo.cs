using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Enemigo : AnimaIA
    {
        public Enemigo() : base("Sprite/Zombie.png",64) 
        {
            arriba = new Animacion(512, 0, 9);
            abajo = new Animacion(640, 0, 9);
            izquierda = new Animacion(578, 0, 9);
            derecha = new Animacion(704, 0, 9);
        }
    }
}
