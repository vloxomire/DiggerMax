﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiggerMax
{
    class ComoSeJuega : Escena
    {
        Sprite fondo;
        public override void Inicio()
        {
            fondo = new Sprite(new Texture("sprite/NuevoJuego2.png"));
            fondo.Color = Color.Blue;
        }
        public override void Actualizar(float DeltaTime, Vector2i posicionRaton)
        {
            
        }
        public override void Dibujar(RenderWindow ventana)
        {
            ventana.Draw(fondo);
        }
        public override void Destruir()
        {

        }
    }
}