using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class TextoDamage
    {
        private Text damageTexto;
        private int velocidadTexto = 3;
        private bool isActivo = false;
        private float dmgTY;
        private float dmgTX;
        private float dmgTVelY;
        private float dmgTVelX;
        private int mensaje = 0;
        private Anima anima;
        public TextoDamage(Anima anima)
        {
            this.anima = anima;
            this.dmgTY = anima.YPOS_ANIMA;
            this.dmgTX = anima.XPOS_ANIMA;
            this.dmgTVelY = 0;
            this.dmgTVelX = 0;

            var font = new Font("Fuentes/MarioKart.ttf");
            damageTexto = new Text(mensaje.ToString(), font);
            {
                damageTexto.Origin = new Vector2f(damageTexto.GetGlobalBounds().Width / 2, damageTexto.GetGlobalBounds().Height / 2);
                damageTexto.FillColor = Color.White;
            };

        }
        public void Actualizar(float deltaTiempo)
        {
            damageTexto.Position = new Vector2f(anima.XPOS_ANIMA, anima.YPOS_ANIMA);
        }
        public void Renderer(RenderWindow ventana)
        {
            ventana.Draw(damageTexto);
        }
        public void SaltarTexto(float velocidad) 
        {
            damageTexto.Position=new Vector2f(0.0f,velocidad);
        }
        public void SetMensaje(int mensaje)
        {
            this.mensaje = mensaje;
        }
        public int GetMensaje()
        {
            return mensaje;
        }
    }
}
