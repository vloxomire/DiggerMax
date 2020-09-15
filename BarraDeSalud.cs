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
        private float saludActual;
        private float saludMaxima;
        private enum EstadoDeSalud {Normal,Bajo,Critico};
        private EstadoDeSalud actualEstadoDeSalud;
        private RectangleShape barraVerde,barraRoja;
        private float saludDeseada;
        private float anchoDeBarra;
        public BarraDeSalud(float saludActual,float saludMaxima)
        {
            this.saludActual = saludActual;
            this.saludMaxima = saludMaxima;
            anchoDeBarra = saludMaxima;
            saludDeseada = saludActual;
            CargarContenido();
        }
        public void Inicializar() 
        {

        }
        public void CargarContenido()
        {
            //manejar texturas y sprites de acá
            var area = new Vector2f(anchoDeBarra,10f);
            barraVerde = new RectangleShape(area);
            barraVerde.FillColor = Color.Green;
            barraRoja = new RectangleShape(area);
            barraRoja.FillColor = Color.Red;
        }
        public void Update(Anima anima)
        {
            //posiicones de las barras
            barraRoja.Position = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);
            barraVerde.Position = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);

            if (saludDeseada == saludActual)
            { return; }
            if (saludDeseada < saludActual)
            { saludActual--; }
            if (saludDeseada > saludActual)
            { saludActual++; }
            UpdateEstadoSalud();
        }
        public void Draw(RenderWindow ventana, Vector2f posicion)
        {
            ventana.Draw(barraRoja);
            ventana.Draw(barraVerde);
        }
        public void UpdateSalud(float saludActual,float saludMaxima ) 
        {
            saludDeseada = saludActual; //salud restada con daño,si hubiese
            this.saludMaxima = saludMaxima;
        }
        public void UpdateEstadoSalud()
        {
            //En un futuro usar sprite
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
    }
}
