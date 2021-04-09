using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class InGameState : GameState
    {
        private Sprite sprite;

        public InGameState()
        {
            this.sprite = new Sprite(Util.CreateFilledRectangleTexture(Color.Red, 1, 1));
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            sprite.Draw(spriteBatch, Vector2.Zero, new Vector2(800, 600), Vector2.Zero, 0.0f, 0.0f);
            spriteBatch.End();
        }
        
    }
}
