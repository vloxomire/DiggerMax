﻿using SFML.Graphics;
using SFML.System;
using System;
using SFML.Window;
using System.Collections.Generic;

namespace DiggerMax
{
    class ComoSeJuega : Escena
    {
        /// <summary>
        /// 
        /// </summary>
        public float Y_POS_TEXT { get; set; }
        private Personaje personaje;
        private Enemigo enemigo;
        private Mapa mapa;
        private View camara;//camara
        private Color colorPj;
        private Color colorEnem;
        private Font fuente;
        Text danioInfo;
        float textoVelocidad;
        public ComoSeJuega()
        {
            colorPj = new Color();
            colorEnem = new Color();
            fuente = new Font("Fuentes/MarioKart.ttf");
            danioInfo = new Text();
            textoVelocidad = 0.1f;
        }
        public override void Inicio()
        {
            //Camara
            camara = new View(new Vector2f(0, 0), new Vector2f(800, 600));//camara init
            camara = new View(new Vector2f(Juego.width, Juego.height), new Vector2f(Juego.width, Juego.height));
            mapa = new Mapa();//mapa init
            personaje = new Personaje()//pj init
            {
                XPOS_ANIMA = 150.0f,
                YPOS_ANIMA = 0.0f
            };
            colorPj = personaje.GetSprite().Color;
            enemigo = new Enemigo()
            {
                XPOS_ANIMA = Juego.width,
                YPOS_ANIMA = 100.0f,
            };
            colorEnem = enemigo.GetSprite().Color;
            //PATRON DE CAMINATA
            enemigo.PuntoCaminoLista = new List<PuntoCamino>();
            //enemigo.PuntoCaminoLista.Add(new PuntoCamino(0,0));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(Juego.width, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(10, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(10, 110));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(Juego.width, 110));
        }
        public override void Actualizar(float deltaTiempo, Vector2i posicionRaton)
        {
            //En el actualizar del GamePlay un rejunte de los demas actualizar(update),que intervienen en el play*/
            personaje.Actualizar(deltaTiempo);
            enemigo.Actualizar(deltaTiempo);
            NoCaerALaLava(deltaTiempo);
            ChocarEnemigo(deltaTiempo);
            Y_POS_TEXT += textoVelocidad * deltaTiempo;
            danioInfo.Position=new Vector2f(enemigo.XPOS_ANIMA,Y_POS_TEXT);
        }
        public override void Dibujar(RenderWindow ventana)
        {
            //CAMARA
            camara.Center = new Vector2f(personaje.XPOS_ANIMA, personaje.YPOS_ANIMA);//Centro camara en pj
            ventana.SetView(camara);//la ventana es la camara
            mapa.Draw(ventana);
            personaje.Dibujar(ventana);
            //COLISIONES
            enemigo.Dibujar(ventana);
            ventana.Draw(danioInfo);
        }
        public override void Destruir()
        {

        }
        private void NoCaerALaLava(float deltaTiempo)
        {
            FloatRect pjGB = personaje.GetSprite().GetGlobalBounds();
            int valor = 0;
            //Si choco
            if (!SiChocoConLava(ref valor,pjGB)) 
            {
                return;
            }
            FloatRect rect = mapa.GetRectangle(valor).GetGlobalBounds();
            if (pjGB.Intersects(rect))
            {
                DondeChocaPj(personaje.ESTADO_AHORA_PJ,deltaTiempo);
            }
        }
        private bool SiChocoConLava(ref int valor,FloatRect pjGB) 
        {
            for (int i = 0; i < mapa.GetIndice(); i++)
            {
                if (pjGB.Intersects(mapa.GetRectangle(i).GetGlobalBounds()))
                {
                    valor = i;
                    return true;
                }
            }
            return false;
        }
        private void DondeChocaPj(EstadosPj estado, float deltaTiempo)
        {
            switch (estado)
            {
                case EstadosPj.MoverArriba:
                    personaje.YPOS_ANIMA += personaje.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverIzquierda:
                    personaje.XPOS_ANIMA += personaje.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverAbajo:
                    personaje.YPOS_ANIMA -= personaje.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverDerecha:
                    personaje.XPOS_ANIMA -= personaje.GetVelocidad() * deltaTiempo;
                    break;
            }
        }
        private void DondeChocaEnem(EstadosPj estado, float deltaTiempo)
        {
            switch (estado)
            {
                case EstadosPj.MoverArriba:
                    enemigo.YPOS_ANIMA += enemigo.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverIzquierda:
                    enemigo.XPOS_ANIMA += enemigo.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverAbajo:
                    enemigo.YPOS_ANIMA -= enemigo.GetVelocidad() * deltaTiempo;
                    break;
                case EstadosPj.MoverDerecha:
                    enemigo.XPOS_ANIMA -= enemigo.GetVelocidad() * deltaTiempo;
                    break;
            }
        }
        private void ChocarEnemigo(float deltaTiempo)
        {
            FloatRect per = personaje.GetSprite().GetGlobalBounds();
            FloatRect enem = enemigo.GetSprite().GetGlobalBounds();
            EstadosPj estadoPj = personaje.ESTADO_AHORA_PJ;
            EstadosPj estadoEnem = enemigo.ESTADO_AHORA_PJ;

            if (per.Intersects(enem))
            {
                DondeChocaPj(estadoPj, deltaTiempo);
                DondeChocaEnem(estadoEnem, deltaTiempo);
                Atacar(estadoPj);
            }
        }
        private void Atacar(EstadosPj estadosPj) 
        {
            //tendria 2 subfunciones una para atq de pj y otro monster
            if (!Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                enemigo.GetSprite().Color = colorEnem;
                //danioInfo.FillColor = Color.Transparent;
            }
            else
            {
                personaje.ChequeoAtaque(estadosPj);
                enemigo.GetSprite().Color = Color.Red;
                enemigo.VIDA -= personaje.DANIO;
                string vidaData = enemigo.VIDA.ToString();
                danioInfo = new Text(vidaData,fuente);
                danioInfo.FillColor = Color.White;
                danioInfo.Position = new Vector2f(enemigo.XPOS_ANIMA,enemigo.YPOS_ANIMA);
            }
        }
    }
}