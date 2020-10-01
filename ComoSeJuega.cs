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
        private bool seChocan;
        private bool isGolpe;
        private Mapa mapa;
        private View camara;//camara
        private Color colorPj;
        private Color colorEnem;
        private RectangleShape rectGameOver;
        private Font fuente;
        private Text text, textColision,txtGameOver;
        Font font = new Font("Fuentes/MarioKart.ttf");
        private string tempo;
        private Clock tiempoGameOver;
        private bool boolActivarTiempoGameOver, boolDibujarGameOver, boolCerraVentana;
        public ComoSeJuega()
        {
            colorPj = new Color();
            colorEnem = new Color();
            listaTextoEnem = new List<TextoPantalla>();
            isActivo = false;
            seChocan = false;
            isGolpe = false;
            boolActivarTiempoGameOver=false;
            boolDibujarGameOver = false;
            boolCerraVentana = false;
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

            //avizo GameOver
            rectGameOver = new RectangleShape(new Vector2f(100f, 100f))
            {
                FillColor = Color.Black,
                Position=new Vector2f(personaje.XPOS_ANIMA,personaje.YPOS_ANIMA),
            };
            txtGameOver = new Text("GAME OVER, viejo!!!", font)
            {
                Position = new Vector2f(rectGameOver.Position.X +5,rectGameOver.Position.Y +5),
            };
            
        }

        public override void Actualizar(float deltaTiempo, Vector2i posicionRaton)
        {

            //En el actualizar del GamePlay un rejunte de los demas actualizar(update),que intervienen en el play*/
            personaje.Actualizar(deltaTiempo);
            enemigo.Actualizar(deltaTiempo);
            NoCaerALaLava(deltaTiempo);

            string vidaData = enemigo.VIDA.ToString();
            textoDamage.Actualizar(deltaTiempo, vidaData, isActivo, personaje, enemigo);

         

            //texto de colision
            string coli = seChocan.ToString();
            textColision = new Text(coli, font);
            textColision.Scale = new Vector2f(0.8f, 0.8f);
            Vector2f enemigoPosicion = new Vector2f(enemigo.XPOS_ANIMA, enemigo.YPOS_ANIMA - 70);
            textColision.Position = enemigoPosicion;

            seChocan = ChequeoColision(deltaTiempo);
            if (seChocan && enemigo.GetClock().ElapsedTime.AsSeconds() > enemigo.NEXTATTACK)
            {
                //de donde es la colision
                /*switch ()
                {
                    case 
                    default:
                }*/

                //enemigo.ESTADO_AHORA_PJ=EstadosPj.
                Console.WriteLine("VidaPj:" + personaje.VIDA);
                personaje.RecibeDano(enemigo.DANIO);
                Console.WriteLine("VidaPj:" + personaje.VIDA);
                Console.WriteLine("NextAttack:" + enemigo.NEXTATTACK);
                enemigo.NEXTATTACK += 3;
                Console.WriteLine("NextAttack:" + enemigo.NEXTATTACK);

                if (enemigo.GetClock().ElapsedTime.AsSeconds() > 10)
                {
                    enemigo.NEXTATTACK = 0;
                }
            }
            barraDeSaludEne.Update(deltaTiempo,enemigo);
            barraDeSaludPer.Update(deltaTiempo,personaje);
            //Verifico resultados
            GestorVidas();
   
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
            //ventana.Draw(textColision);
            barraDeSaludEne.Draw(ventana, new Vector2f(enemigo.XPOS_ANIMA, enemigo.YPOS_ANIMA + 10f));
            barraDeSaludPer.Draw(ventana, new Vector2f(personaje.XPOS_ANIMA, personaje.YPOS_ANIMA + 10f));
            //ventanaGameOver
            DibujarGameOver(ventana);
            CerrarVentana(ventana);
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
        private bool ChequeoColision(float deltaTiempo)
        {
            FloatRect per = personaje.GetSprite().GetGlobalBounds();
            FloatRect enem = enemigo.GetSprite().GetGlobalBounds();
            EstadosPj estadoPj = personaje.ESTADO_AHORA_PJ;
            EstadosPj estadoEnem = enemigo.ESTADO_AHORA_PJ;

            if (per.Intersects(enem))
            {
                Clock tiempoCombate = new Clock();
                enemigo.CONTACTO = true;
                isActivo = true;
                seChocan = true;
                DondeChocaPj(estadoPj, deltaTiempo);
                DondeChocaEnem(estadoEnem, deltaTiempo);
                TeclasAtaque(estadoPj, deltaTiempo);
            }
            else
            {
                seChocan = false;
            }
            return seChocan;
        }
        private void TeclasAtaque(EstadosPj estadosPj, float deltaTiempo)
        {
            if (!Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                enemigo.GetSprite().Color = colorEnem;
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
                enemigo.RecibeDano(personaje.DANIO);
                //Rematar(enemigo, EstaMuerto(enemigo));
            }
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
        private void Luchar(bool seChocan)
        {
            if (!seChocan)
            {
                return;
            }
            else
            {
                //enemy attack frequency
                if (!enemigo.ATACO)
                {
                    return;
                }
                else 
                {
                    personaje.VIDA = personaje.VIDA - enemigo.DANIO;
                    barraDeSaludPer.UpdateSalud(personaje.VIDA, personaje.VIDAMAX);
                    enemigo.ATACO = false;
                }
            }
            //chequeo bbarras de vida
            if (personaje.VIDA < 0)
            {
                personaje.VIDA = 0;
            }
            else if (enemigo.VIDA < 0)
            {
                enemigo.VIDA = 0;
            }
            
            barraDeSaludEne.UpdateSalud(enemigo.VIDA, enemigo.VIDAMAX);
        }
        public override void Destruir()
        {
            throw new NotImplementedException();
        }
        private void GestorVidas() 
        {
            if (!personaje.EstaVivo(personaje)) 
            {
                if (!boolActivarTiempoGameOver)
                {
                    tiempoGameOver = new Clock();
                    boolActivarTiempoGameOver = true;
                    boolDibujarGameOver = true;
                }
                if (tiempoGameOver.ElapsedTime.AsSeconds() >5)
                {
                    boolCerraVentana = true;
                }
            }
        }
        private void DibujarGameOver(RenderWindow ventana) 
        {
            if (boolDibujarGameOver)
            {
                ventana.Draw(rectGameOver);
                ventana.Draw(txtGameOver);
            }
        }

        private void CerrarVentana(RenderWindow ventana) 
        {
            if (boolCerraVentana)
            {
                ventana.Close();
            }
        }
    }
}