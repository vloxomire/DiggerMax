using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace DiggerMax
{
    class Personaje : Anima
    {
        EstadosPj comoAtaca;
        public Personaje() :base("Sprite/orc.png",64)//256alto /4 filas =64
        {
            //Movimiento
            arriba = new Animacion(512,0,9);
            abajo = new Animacion(640,0,9);
            izquierda = new Animacion(578,0,9);
            derecha = new Animacion(704,0,9);
            //Ataque
            atqArb = new Animacion(775,0,6);//cada 64
            atqIzq = new Animacion(839, 0, 6);
            atqAbj = new Animacion(903,0,6);
            atqDer = new Animacion(967,0,6);
            //Magia
            MagiaArribaAnim = new Animacion(0, 0, 7);
            MagiaIzquierdaAnim = new Animacion(64, 0, 7);
            MagiaAbajoAnim = new Animacion(128, 0, 7);
            MagiaDerechaAnim = new Animacion(192, 0, 7);
            //Morir o hacerse el muerto y Levantarse
            //Version como puedo invertir el bucle para que se levante
            MorirAnim = new Animacion(1280, 0, 6);
            LevantarseAnim = new Animacion(1280,192,6);//ARREGLAR,No cambia el numero de izquierda

            velocidadDeMover = 400;//150
            velocidadDeAnimacion = 0.05f;

            comoAtaca = EstadosPj.idle;
        }
        public override void Actualizar(float DeltaTime)
        {
            this.ESTADO_AHORA_PJ = EstadosPj.idle;
            //Si se toca el click izquierdo del mouse ataca
            ChequeoMorirLevantarse();
            if (this.ESTADO_AHORA_PJ != EstadosPj.idle) 
            {
                comoAtaca = this.ESTADO_AHORA_PJ;
            }
            ChequeoAtaque(comoAtaca);
            ChequeoMagia();
           if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                //Colision Pared Top
                if (YPOS_ANIMA < 0.0f - GetIntRect().Height / 3.6f)
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.idle;
                }
                else 
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.MoverArriba;
                }
                //Colision con cajas
                /*if (GetIntRect().Intersects(Mapa.rectangulo))
                {

                }*/
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                //Colision Pared Bot
                if (YPOS_ANIMA > Juego.height - GetIntRect().Height /2.0f)
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.idle;
                }
                else 
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.MoverAbajo;
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                //Colision Pared Left
                if (XPOS_ANIMA < 0.0f - GetIntRect().Width/2.0f)
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.idle;
                }
                else 
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.MoverIzquierda;
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                //Colision Pared Right
                if (XPOS_ANIMA > Juego.width - GetIntRect().Width / 4.0f)
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.idle;
                }
                else 
                {
                    this.ESTADO_AHORA_PJ = EstadosPj.MoverDerecha;
                }
            }
            base.Actualizar(DeltaTime);
        }
        public void ChequeoAtaque(EstadosPj estado) 
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                switch (estado)
                {
                    case EstadosPj.MoverArriba:
                        this.ESTADO_AHORA_PJ = EstadosPj.AtacarArriba;
                        break;
                    case EstadosPj.MoverIzquierda:
                        this.ESTADO_AHORA_PJ = EstadosPj.AtacarIzquierda;
                        break;
                    case EstadosPj.MoverAbajo:
                        this.ESTADO_AHORA_PJ = EstadosPj.AtacarAbajo;
                        break;
                    case EstadosPj.MoverDerecha:
                        this.ESTADO_AHORA_PJ = EstadosPj.AtacarDerecha;
                        break;
                }
            }
        }
        private void ChequeoMagia() 
        {
            //Se toca el boton y debe hacer toda la animacion
            if (Keyboard.IsKeyPressed(Keyboard.Key.M))
            {
                this.ESTADO_AHORA_PJ = EstadosPj.MagiaAbajo;
            }
        }
        private void ChequeoMorirLevantarse()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
            {
                this.ESTADO_AHORA_PJ = EstadosPj.Morir;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                this.ESTADO_AHORA_PJ = EstadosPj.Levantarse;
            }
        }
    }
}
