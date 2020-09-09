using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    public enum EstadosPj
    {
        idle, MoverArriba, MoverAbajo, MoverIzquierda, MoverDerecha
            ,AtacarArriba, AtacarAbajo, AtacarIzquierda,AtacarDerecha,
            MagiaArriba, MagiaAbajo, MagiaIzquierda, MagiaDerecha,
            Morir,Levantarse
    }
    abstract class Anima
    {
        //Propiedad
        public float XPOS_ANIMA { get; set; }
        public float YPOS_ANIMA { get; set; }
        public int VIDA { get; set; }
        public int DANIO { get; set; }
        public bool MUERTO { get; set; }
        public EstadosPj ESTADO_AHORA_PJ { get; set; }//objeto para acceder al enumerador
        //Campos
        private Sprite sprite;
        private IntRect spriteRect;
        private int tamanioDelFrame;
        private Clock clock;

        protected Animacion arriba,atqArb;
        protected Animacion abajo,atqAbj;
        protected Animacion izquierda,atqIzq;
        protected Animacion derecha,atqDer;

        protected Animacion MagiaArribaAnim;
        protected Animacion MagiaAbajoAnim;
        protected Animacion MagiaIzquierdaAnim;
        protected Animacion MagiaDerechaAnim;

        protected Animacion MorirAnim;
        protected Animacion LevantarseAnim;

        protected float velocidadDeMover = 50;
        protected float velocidadDeAnimacion = 0.1f;

        //Constructor
        public Anima(string nombreDelArchivo, int tamanioDelFrame)
        {
            this.tamanioDelFrame = tamanioDelFrame;
            spriteRect = new IntRect(0, 0, tamanioDelFrame, tamanioDelFrame);
            sprite = new Sprite(new Texture(nombreDelArchivo), spriteRect);
            clock = new Clock();
            VIDA = 100;
            DANIO = 2;
            Time tiempo = clock.Restart();
        }
        public virtual void Actualizar(float deltaTiempo)
        {
            //chequeo el movimiento
            Animacion animacionAhora = null;
            switch (ESTADO_AHORA_PJ)
            {
                case EstadosPj.MoverArriba:
                    animacionAhora = arriba;
                    YPOS_ANIMA -= velocidadDeMover * deltaTiempo;
                    break;
                case EstadosPj.MoverAbajo:
                    animacionAhora = abajo;
                    YPOS_ANIMA += velocidadDeMover * deltaTiempo;
                    break;
                case EstadosPj.MoverIzquierda:
                    animacionAhora = izquierda;
                    XPOS_ANIMA -= velocidadDeMover * deltaTiempo;
                    break;
                case EstadosPj.MoverDerecha:
                    animacionAhora = derecha;
                    XPOS_ANIMA += velocidadDeMover * deltaTiempo;
                    break;
                case EstadosPj.AtacarArriba:
                    animacionAhora = atqArb;
                    break;
                case EstadosPj.AtacarAbajo:
                    animacionAhora = atqAbj;
                    break;
                case EstadosPj.AtacarIzquierda:
                    animacionAhora = atqIzq;
                    break;
                case EstadosPj.AtacarDerecha:
                    animacionAhora = atqDer;
                    break;
                case EstadosPj.MagiaArriba:
                    animacionAhora = MagiaArribaAnim;
                    break;
                case EstadosPj.MagiaAbajo:
                    animacionAhora = MagiaAbajoAnim;
                    break;
                case EstadosPj.MagiaIzquierda:
                    animacionAhora = MagiaIzquierdaAnim;
                    break;
                case EstadosPj.MagiaDerecha:
                    animacionAhora = MagiaDerechaAnim;
                    break;
                case EstadosPj.Morir:
                    animacionAhora = MorirAnim;
                    break;
                case EstadosPj.Levantarse:
                    animacionAhora = LevantarseAnim;
                    break;
            }
            sprite.Position = new Vector2f(XPOS_ANIMA, YPOS_ANIMA);//lo pasa a la posicion del pj actual

            //Animacion x frame
            if (clock.ElapsedTime.AsSeconds() > velocidadDeAnimacion)
            {
                if (animacionAhora != null)
                {
                    spriteRect.Top = animacionAhora.setArriba;
                    if (spriteRect.Left == (animacionAhora.numeroDeFrames - 1) * tamanioDelFrame)
                    {
                        spriteRect.Left = 0;
                    }
                    else
                    {
                        spriteRect.Left += tamanioDelFrame;
                    }
                }
                clock.Restart();
            }
            sprite.TextureRect = spriteRect;
        }
        public void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(sprite);
        }
        public IntRect GetIntRect() 
        {
            return spriteRect;
        }
        public Sprite GetSprite() 
        {
            return sprite;
        }
        public float GetVelocidad() 
        {
            return velocidadDeMover;
        }
    }
}
