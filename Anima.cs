using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    public enum EstadosPj 
    {
        idle,MoverArriba, MoverAbajo, MoverIzquierda, MoverDerecha
    }
    abstract class Anima
    {
        //Propiedad
        public float XPOS_ANIMA { get; set; }
        public float YPOS_ANIMA { get; set; }
        public EstadosPj ESTADO_AHORA_PJ { get; set; }//objeto para acceder al enumerador

        //Campos
        private Sprite sprite;
        private IntRect spriteRect;
        private int tamanioDelFrame;
        private Clock clock;

        protected Animacion arriba;
        protected Animacion abajo;
        protected Animacion izquierda;
        protected Animacion derecha;

        protected float velocidadDeMover = 50;
        protected float velocidadDeAnimacion=0.1f;

        //Constructor
        public Anima(string nombreDelArchivo,int tamanioDelFrame) 
        {
            this.tamanioDelFrame = tamanioDelFrame;
            spriteRect = new IntRect(0, 0, tamanioDelFrame, tamanioDelFrame);
            sprite = new Sprite(new Texture(nombreDelArchivo),spriteRect);

            clock = new Clock();
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
            }
            sprite.Position = new Vector2f(XPOS_ANIMA,YPOS_ANIMA);//lo pasa a la posicion del pj actual
            
            //Animacion x frame
            if (clock.ElapsedTime.AsSeconds()>velocidadDeAnimacion)
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
    }
}
