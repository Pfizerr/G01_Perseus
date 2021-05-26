using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Camera
    {

        private Matrix translationMatrix; // This was mentioned as "not needed", however, it is. Else you will have to recreate the matrix every time you update its' translation (bad performance).

        public Camera(GameWindow window)
        {
            this.Window = window;
            this.translationMatrix = Matrix.Identity;
        }

        public GameWindow Window
        {
            get;
            private set;
        }

        public Rectangle Viewport
        {
            get{ return Window.ClientBounds; }
        }


        public Player FollowTarget
        {
            get;
            set;
        }

        public Matrix Translation => translationMatrix;

        public Vector2 CenterPosition
        {
            get
            {
                return FollowTarget.Position;
            }
        }


        public void Update()
        {
            this.translationMatrix.Translation = new Vector3(-FollowTarget.Position.X + (Viewport.Width * 0.5f) - 25, -FollowTarget.Position.Y + (Viewport.Height * 0.5f) - 25, 0.0f);
        }

    }
}