using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace DiggerMax
{
    class Personaje : Anima
    {
        public Personaje() :base("Sprite/orc.png",64)//256alto /4 filas =64
        {
            arriba = new Animacion(512,0,9);
            abajo = new Animacion(640,0,9);
            izquierda = new Animacion(578,0,9);
            derecha = new Animacion(704,0,9);

            velocidadDeMover = 150;
            velocidadDeAnimacion = 0.05f;
        }
        public override void Actualizar(float DeltaTime)
        {
            this.ESTADO_AHORA_PJ = EstadosPj.idle;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                    this.ESTADO_AHORA_PJ = EstadosPj.MoverArriba;   
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { this.ESTADO_AHORA_PJ = EstadosPj.MoverAbajo; }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { this.ESTADO_AHORA_PJ = EstadosPj.MoverIzquierda; }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { this.ESTADO_AHORA_PJ = EstadosPj.MoverDerecha; }

            base.Actualizar(DeltaTime);
        }
        public override void Colisiones() 
        {
            //chquear como era el override
            base;
        }
    }
}
