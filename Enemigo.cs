using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Enemigo : AnimaIA
    {
        public Enemigo() : base("Sprite/arachne.png",100) 
        {
            arriba = new Animacion(0,0,3);
            abajo = new Animacion(280,0,03);
            izquierda = new Animacion(140,0,3);
            derecha = new Animacion(320,0,3);
        }
    }
}
