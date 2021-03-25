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

        private Matrix translationMatrix;
        private Rectangle viewport;

        public Camera()
        {
            this.translationMatrix = Matrix.Identity;
            this.viewport = new Rectangle(0, 0, 0, 0);
        }

        public Rectangle Viewport
        {
            get{ return viewport; }
            set{ viewport = value; }
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
                return Player.Position;
            }
        }


        public void Update()
        {
            this.translationMatrix.Translation = new Vector3(-Player.Position.X+(Viewport.Width * 0.5f)-25, -Player.Position.Y + (Viewport.Height * 0.5f)-25, 0.0f);
        }

    }
}
