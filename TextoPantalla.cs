using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class TextoPantalla
    {
        private Text nombreTexto1, nombreTexto2, text;
        private float velocidad = 3.0f;
        private bool isActivo = false;
        private string nombresPj, nombreEnem;
        public float X_Text;
        public float Y_Text;
        public float YPOS_TEXT;
        public float XPOS_TEXT;
        private string mensaje;
        private Anima anima;
        float PosX = 10;
        Font font = new Font("Fuentes/MarioKart.ttf");
        public TextoPantalla(Anima anima, string mensaje)
        {
            this.anima = anima;
            this.YPOS_TEXT = anima.YPOS_ANIMA;
            this.XPOS_TEXT = anima.XPOS_ANIMA;
            //NOMBRES
            
            nombreTexto2 = new Text(anima.GetName(), font);

            //textoPrueba
            X_Text = anima.XPOS_ANIMA;
            Y_Text = 10.0f;
            var textLugar = new Vector2f(X_Text, Y_Text);
            text = new Text("0", font);
            text.FillColor = Color.Yellow;
            text.Position = textLugar;//pos init
        }

        public void Actualizar(float deltaTiempo, string vidaData, bool isActivo,Personaje personaje,Enemigo enemigo)
        {
            nombreTexto1 = new Text(personaje.GetName(), font);
            nombreTexto1.Position = new Vector2f(personaje.GetSprite().GetGlobalBounds().Left, personaje.GetSprite().GetGlobalBounds().Top-30);

            nombreTexto2.Position = new Vector2f(enemigo.XPOS_ANIMA, enemigo.YPOS_ANIMA-30);
            if (isActivo)
            {
                text.FillColor = Color.Yellow;
                YPOS_TEXT -= 20.0f * deltaTiempo;
                text.Position = new Vector2f(anima.XPOS_ANIMA, YPOS_TEXT);
            }
            else
            {
                text.FillColor = Color.Transparent;
            }
            if (text.Position.Y > 80f)
            {
                text.FillColor = Color.Red;
            }
        }
        public void Draw(RenderWindow ventana)
        {

            ventana.Draw(text);
            ventana.Draw(nombreTexto1);
            ventana.Draw(nombreTexto2);
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
