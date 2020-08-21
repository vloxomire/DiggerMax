using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace DiggerMax
{
    abstract class GameObject
    {
        protected Sprite renderer;
        private bool active = true;
        public GameObject(Texture tex) 
        {
            renderer = new Sprite(tex);
        }
        public GameObject(Texture tex,float x, float y) 
        {
            renderer = new Sprite(tex);
            renderer.Position = new Vector2f(x,y);
        }
        public abstract void Actualizar(float DeltaTime);
        public Sprite GetRenderer() 
        {
            return renderer;
        }
        public FloatRect GetGlobalBounds() 
        {
            return renderer.GetGlobalBounds();
        }
        public void SetActive(bool status) 
        {
            active = status;
        }
        public bool isActive() 
        {
            return active;
        }
        public void FlipX()
        {
            renderer.TextureRect = new IntRect(renderer.TextureRect.Width , 0 , -renderer.TextureRect.Width, renderer.TextureRect.Height);
        }
        public void FlipY()
        {
            renderer.TextureRect = new IntRect(0,renderer.TextureRect.Height, renderer.TextureRect.Width, -renderer.TextureRect.Height);
        }
    }
}
