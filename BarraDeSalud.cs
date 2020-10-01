using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DiggerMax
{
    class BarraDeSalud
    {
        private enum EstadoDeSalud { Normal, Bajo, Critico };
        private EstadoDeSalud actualEstadoDeSalud;
        private float saludDeseada;
        private float anchoDeBarra;
        private float saludActual;
        private float saludMaxima;
        private float velAnimCorz = 0.1f;
        private IntRect corazonRect;
        private Vector2f barraPosicion;
        private Vector2f corazonPosicion;
        private RectangleShape barraVerde, barraRoja;
        private Sprite corazonSprite;
        private Anima anima;
        private Clock relog;
        private Animacion corazonAnim;
        public BarraDeSalud(float saludActual, float saludMaxima, Anima anima)
        {
            //Se podria sacar por parametro el salud Act y Max , lo podria pedir al anima
            this.saludActual = saludActual;
            this.saludMaxima = saludMaxima;
            this.anima = anima;
            anchoDeBarra = anima.VIDAMAX;
            saludDeseada = saludActual;
            relog = new Clock();
            Time tiempo = relog.Restart();
            corazonAnim = new Animacion(0, 0, 6);
            CargarContenido();
        }
        public void Inicializar()
        {
        }
        public void CargarContenido()
        {
            //manejar texturas y sprites de acá
            //Barra
            var area = new Vector2f(anchoDeBarra, 10f);
            barraVerde = new RectangleShape(area);
            barraVerde.FillColor = Color.Green;
            barraRoja = new RectangleShape(area);
            barraRoja.FillColor = Color.Red;
            //Corazon
            corazonRect = new IntRect(0, 0, 64, 64);
            corazonSprite = new Sprite(new Texture("Sprite/heart64x64.png"), corazonRect);
            corazonSprite.Origin = new Vector2f(corazonSprite.GetGlobalBounds().Width / 2, corazonSprite.GetGlobalBounds().Height / 2);
            corazonSprite.Scale = new Vector2f(0.5f, 0.5f);
        }
        public void Update(float deltaTime,Anima anima)
        {
            //posiicones de las barras
            barraRoja.Position = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);
            barraVerde.Position = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);
            
            //Corazon
            corazonPosicion = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);
            corazonSprite.Position = corazonPosicion;
            //Corazon Animacion
            if (relog.ElapsedTime.AsSeconds() > velAnimCorz)
            {
                if (corazonAnim != null)
                {
                    corazonRect.Top = corazonAnim.setArriba;
                    if (corazonRect.Left == (corazonAnim.numeroDeFrames - 1) * 64)
                    {
                        corazonRect.Left = 0;
                    }
                    else
                    {
                        corazonRect.Left += 64;
                    }
                }
                relog.Restart();
            }
            corazonSprite.TextureRect = corazonRect;

            UpdateBarraSaludAncho();

            if (saludDeseada == saludActual)
            { return; }
            /*if (saludDeseada < saludActual)
            { saludActual--; }
            if (saludDeseada > saludActual)
            { saludActual++; }*/
            UpdateEstadoSalud();
            if (saludActual < 0)
            {
                saludActual = 0;
            }
            
        }
        public void Draw(RenderWindow ventana, Vector2f posicion)
        {
            if (anima.MUERTO)
            {

            }
            else 
            {
                ventana.Draw(barraRoja);
                ventana.Draw(barraVerde);
                ventana.Draw(corazonSprite);
            }
            
        }
        public void UpdateSalud(float saludActual, float saludMaxima)
        {
            saludDeseada = saludActual; //salud restada con daño,si hubiese
            this.saludMaxima = saludMaxima;
        }
        public void UpdateEstadoSalud()
        {
            //En un futuro usar sprite,se acelera la velocidad del corazon
            float porcentaje = saludActual / saludMaxima;
            if (porcentaje < 0.2)
            {
                actualEstadoDeSalud = EstadoDeSalud.Critico;
            }
            else if (porcentaje < 0.5)
            {
                actualEstadoDeSalud = EstadoDeSalud.Bajo;
            }
            else
            {
                actualEstadoDeSalud = EstadoDeSalud.Normal;
            }
            anchoDeBarra = (saludActual / saludMaxima) * 50;
        }
        public void UpdateBarraSaludAncho()
        {
            barraVerde.Size = new Vector2f(anima.VIDA, 10f);
        }
    }
}
