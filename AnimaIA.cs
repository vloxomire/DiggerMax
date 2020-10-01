using SFML.System;
using System;
using System.Collections.Generic;

namespace DiggerMax
{
    class AnimaIA : Anima
    {
        public List<PuntoCamino> PuntoCaminoLista { get; set; }
        private int siguientePuntoIndex = 1;
        
        private Clock clockIA;
        public AnimaIA(string nombreArchivo, int tamanioFrame):base(nombreArchivo,tamanioFrame)
        {
            clockIA = new Clock();
        }
        public override void Actualizar(float deltaTiempo)
        {
            SeguirCamino(MUERTO);
            base.Actualizar(deltaTiempo);
        }
        public void SeguirCamino(bool muerto) 
        {
            if (!MUERTO)
            {
                if (clockIA.ElapsedTime.AsSeconds() > 0.5f)
                {
                    if (PuntoCaminoLista != null)
                    {
                        PuntoCamino siguienteCamino = PuntoCaminoLista[siguientePuntoIndex];
                        float xDiferencia = siguienteCamino.XPos - this.XPOS_ANIMA;
                        float yDiferencia = siguienteCamino.YPos - this.YPOS_ANIMA;
                        float absXDiferencia = Math.Abs(xDiferencia);
                        float absYDiferencia = Math.Abs(yDiferencia);

                        if (absXDiferencia < 10 && absYDiferencia < 10)
                        {
                            if (siguientePuntoIndex < PuntoCaminoLista.Count - 1)
                            {
                                siguientePuntoIndex++;
                            }
                            else
                            {
                                siguientePuntoIndex = 0;
                            }
                        }
                        if (absXDiferencia > absYDiferencia)
                        {
                            if (xDiferencia > 0)
                            {
                                this.ESTADO_AHORA_PJ = EstadosPj.MoverDerecha;
                            }
                            if (xDiferencia < 0)
                            {
                                this.ESTADO_AHORA_PJ = EstadosPj.MoverIzquierda;
                            }
                        }
                        else
                        {
                            if (yDiferencia > 0)
                            {
                                this.ESTADO_AHORA_PJ = EstadosPj.MoverAbajo;
                            }
                            if (yDiferencia < 0)
                            {
                                this.ESTADO_AHORA_PJ = EstadosPj.MoverArriba;
                            }
                        }
                    }
                    clockIA.Restart();
                }
            }
            else 
            {
                PuntoCaminoLista = null;
            }
        }
    }
}
