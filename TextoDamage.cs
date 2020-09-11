using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class TextoDamage
    {
        private Text damageTexto,text;
        private int velocidad = 3;
        private bool isActivo = false;
        public float YPOS_TEXT;
        public float XPOS_TEXT;
        private string mensaje;
        private Anima anima;
        float PosX = 10;
        public TextoDamage(Anima anima,string mensaje)
        {
            this.anima = anima;
            this.YPOS_TEXT = anima.YPOS_ANIMA;
            this.XPOS_TEXT = anima.XPOS_ANIMA;

            var font = new Font("Fuentes/MarioKart.ttf");
            damageTexto = new Text(mensaje.ToString(), font);
            {
                damageTexto.Origin = new Vector2f(damageTexto.GetGlobalBounds().Width / 2, damageTexto.GetGlobalBounds().Height / 2);
                damageTexto.FillColor = Color.White;
            };
            //textoPrueba
            text = new Text("0",font);
            text.FillColor = Color.Yellow;
            
            text.Position = new Vector2f(PosX, 10f);//pos init
        }
        public void Actualizar(float deltaTiempo,string vidaData)
        {
            if (isActivo)
            {
                SetMensaje(vidaData);
                
                text.Position += new Vector2f(velocidad * deltaTiempo, 10f);
                //listaTextoEnem.Add(textoDamage);
            }
            text.Position+=new Vector2f(velocidad* deltaTiempo,30f * deltaTiempo);
            if (text.Position.Y > 60.0f)
            {
                text.FillColor = Color.Transparent;
            }
        }
        public void Draw(RenderWindow ventana)
        {
            /*for (int i = 0; i < listaTextoEnem.Count; i++)
            {
                listaTextoEnem[i].Draw(ventana);
                listaTextoEnem[i].SaltarTexto();
            }*/
            ventana.Draw(damageTexto);
            isActivo = false;
            ventana.Draw(text);
        }
        public void SaltarTexto() 
        {
            damageTexto.Position=new Vector2f(anima.XPOS_ANIMA, velocidad);
        }
        public void SetMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }
        public string GetMensaje()
        {
            return mensaje;
        }
        public void setIsActivo(bool isActivo) 
        {
            this.isActivo = isActivo;
        }
        public bool GetIsActivo() 
        {
            return isActivo;
        }
    }
}
