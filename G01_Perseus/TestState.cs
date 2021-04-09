using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class TestState : GameState
    {

        private Sprite sprite;

        public TestState()
        {
            this.sprite = new Sprite(Util.CreateFilledRectangleTexture(Color.Blue, 1, 1));
            Transparent = true;
        }


        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            sprite.Draw(spriteBatch, new Vector2(50,50), new Vector2(100, 100), Vector2.Zero, 0.0f, 0.0f);
            spriteBatch.End();
        }

    }
}
