using SFML.Graphics;
using SFML.System;
using System;
using SFML.Window;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Reflection.PortableExecutable;

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
        TextoPantalla textoDamage;
        BarraDeSalud barraDeSaludEne, barraDeSaludPer;
        private List<TextoPantalla> listaTextoEnem;
        private bool isActivo;
        private bool enCombate;
        private bool isGolpe;
        private Mapa mapa;
        private View camara;//camara
        private Color colorPj;
        private Color colorEnem;
        private Font fuente;
        private Text text;
        Font font = new Font("Fuentes/MarioKart.ttf");
        private string tempo;
        public ComoSeJuega()
        {
            colorPj = new Color();
            colorEnem = new Color();
            listaTextoEnem = new List<TextoPantalla>();
            isActivo = false;
            enCombate = false;
            isGolpe = false;
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
            textoDamage = new TextoPantalla(enemigo, "");
            barraDeSaludEne = new BarraDeSalud(enemigo.VIDA, enemigo.VIDAMAX, enemigo);
            barraDeSaludPer = new BarraDeSalud(personaje.VIDA, personaje.VIDAMAX, personaje);
            //PATRON DE CAMINATA
            enemigo.PuntoCaminoLista = new List<PuntoCamino>();
            //enemigo.PuntoCaminoLista.Add(new PuntoCamino(0,0));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(Juego.width, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(10, 100));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(10, 110));
            enemigo.PuntoCaminoLista.Add(new PuntoCamino(Juego.width, 110));
            //ayuda de relog
            text = new Text(tempo, font);
            text.Position = new Vector2f(0, 0);
        }
        public override void Actualizar(float deltaTiempo, Vector2i posicionRaton)
        {
            
            //En el actualizar del GamePlay un rejunte de los demas actualizar(update),que intervienen en el play*/
            personaje.Actualizar(deltaTiempo);
            enemigo.Actualizar(deltaTiempo);
            NoCaerALaLava(deltaTiempo);
            ChocarEnemigo(deltaTiempo);
            string vidaData = enemigo.VIDA.ToString();
            textoDamage.Actualizar(deltaTiempo, vidaData, isActivo, personaje, enemigo);
            barraDeSaludEne.Update(deltaTiempo);
            barraDeSaludPer.Update(deltaTiempo);
            Combate();
        }
        public override void Dibujar(RenderWindow ventana)
        {
            //CAMARA
            camara.Center = new Vector2f(personaje.XPOS_ANIMA, personaje.YPOS_ANIMA);//Centro camara en pj
            ventana.SetView(camara);//la ventana es la camara
            mapa.Draw(ventana);
            ventana.Draw(text);
            personaje.Dibujar(ventana);
            //COLISIONES
            enemigo.Dibujar(ventana);
            textoDamage.Draw(ventana);
            barraDeSaludEne.Draw(ventana, new Vector2f(enemigo.XPOS_ANIMA, enemigo.YPOS_ANIMA + 10f));
            barraDeSaludPer.Draw(ventana, new Vector2f(personaje.XPOS_ANIMA, personaje.YPOS_ANIMA + 10f));
        }
        private void NoCaerALaLava(float deltaTiempo)
        {
            FloatRect pjGB = personaje.GetSprite().GetGlobalBounds();
            int valor = 0;
            //Si choco
            if (!SiChocoConLava(ref valor, pjGB))
            {
                return;
            }
            FloatRect rect = mapa.GetRectangle(valor).GetGlobalBounds();
            if (pjGB.Intersects(rect))
            {
                DondeChocaPj(personaje.ESTADO_AHORA_PJ, deltaTiempo);
            }
        }
        private bool SiChocoConLava(ref int valor, FloatRect pjGB)
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
                Clock tiempoCombate = new Clock();
                isActivo = true;
                enCombate = true;
                DondeChocaPj(estadoPj, deltaTiempo);
                DondeChocaEnem(estadoEnem, deltaTiempo);
                TeclasAtaque(estadoPj, deltaTiempo);
            }
            else
            {
                enCombate = false;
            }
        }
        private void TeclasAtaque(EstadosPj estadosPj, float deltaTiempo)
        {
            //tendria 2 subfunciones una para atq de pj y otro monster
            if (!Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                enemigo.GetSprite().Color = colorEnem;
                isGolpe = false;
                //danioInfo.FillColor = Color.Transparent;
            }
            else
            {
                FloatRect per = personaje.GetSprite().GetGlobalBounds();
                FloatRect enem = enemigo.GetSprite().GetGlobalBounds();
                EstadosPj estadoPj = personaje.ESTADO_AHORA_PJ;
                EstadosPj estadoEnem = enemigo.ESTADO_AHORA_PJ;
                if (per.Intersects(enem))
                {
                    textoDamage.setIsActivo(true);
                    switch (estadoPj)
                    {
                        case EstadosPj.MoverArriba:
                            personaje.ESTADO_AHORA_PJ = EstadosPj.AtacarArriba;
                            break;
                        case EstadosPj.MoverIzquierda:
                            personaje.ESTADO_AHORA_PJ = EstadosPj.AtacarIzquierda;
                            break;
                        case EstadosPj.MoverAbajo:
                            personaje.ESTADO_AHORA_PJ = EstadosPj.AtacarAbajo;
                            break;
                        case EstadosPj.MoverDerecha:
                            personaje.ESTADO_AHORA_PJ = EstadosPj.AtacarDerecha;
                            break;
                    }
                    personaje.Actualizar(deltaTiempo);
                }


                enemigo.GetSprite().Color = Color.Red;
                isGolpe = true;
                //Pregunto por vida, si enemigo esta en 0 o meno poder rematarlo, con un boole para saber?
                Rematar(enemigo, EstaMuerto(enemigo));
            }
        }
        private bool EstaMuerto(Anima anima)
        {
            if (anima.VIDA >= 0)
            {
                return false;
            }
            enemigo.MUERTO = true;
            return true;
        }
        private void Rematar(Anima anima, bool muerto)
        {
            if (!muerto)
            {
                return;
            }
            int i = 0;
            //logica
            enemigo.ESTADO_AHORA_PJ = EstadosPj.Morir;
        }
        private void Combate()
        {

            //en desarrollo
            /*ide: si chocan y se apreto la tecla de ataque.2 condiciones
             Entonces se inicia el combate aparece  un textx en pantalla que diga fight!!!
            depues desaaparece.
            Toma la vida maxima y lo pasa al update de vida que esta chequeando constantemente el daño,
            para repercutir en la barra*/
            if (!enCombate)
            {
                return;
            }
            else
            {
                //Llamar un texto que diga combate o boolean q deja activado el texto
                if (enCombate && isGolpe)
                {
                    enemigo.VIDA -= personaje.DANIO;
                }
                barraDeSaludEne.UpdateSalud(enemigo.VIDA, enemigo.VIDAMAX);
                if (enCombate)
                {

                }
            }
        }
        public override void Destruir()
        {
            throw new NotImplementedException();
        }
    }
}