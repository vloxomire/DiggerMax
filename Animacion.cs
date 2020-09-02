using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class Animacion
    {
        //Campos
        public int setArriba;
        public int setIzquierda;
        public int numeroDeFrames;
        //Constructor
        public Animacion(int setArriba,int setIzquierda,int numeroDeFrames)
        {
            this.setArriba = setArriba;
            this.setIzquierda = setIzquierda;
            this.numeroDeFrames = numeroDeFrames;
        }
    }
}
