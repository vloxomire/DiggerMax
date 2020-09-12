using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DiggerMax
{
    class Enemigo : AnimaIA
    {
        public Enemigo() : base("Sprite/Zombie.png",64) 
        {
            nombre = "ZoMbIe";
            arriba = new Animacion(512, 0, 9);
            abajo = new Animacion(640, 0, 9);
            izquierda = new Animacion(578, 0, 9);
            derecha = new Animacion(704, 0, 9);
            MorirAnim = new Animacion(1280,0,6);
        }
    }
}
