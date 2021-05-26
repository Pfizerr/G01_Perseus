using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class StateStack
    {

        private Stack<GameState> stack = new Stack<GameState>();

        private int amountToPop;

        public void Push(GameState gameState)
        {
            stack.Push(gameState);
        }

        public void Pop()
        {
            //amountToPop++;
            stack.Pop();
        }

        public GameState Peek()
        {
            return stack.Peek();
        }


        public void Update(GameTime gameTime)
        {
            foreach(GameState state in stack.ToList())
            {
                state.Update(gameTime);
                if(!state.Transparent)
                {
                    break;
                }
            }

            /*for(int i = 0; i < amountToPop;i++)
            {
                if(stack.Count <= 0)
                {
                    amountToPop = 0;
                    break;
                }
                stack.Pop();
                amountToPop--;
            }*/
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Stack<GameState> statesToDraw = new Stack<GameState>();
            foreach(GameState state in stack)
            {
                statesToDraw.Push(state);
                if(!state.Transparent)
                {
                    break;
                }
            }
            foreach(GameState state in statesToDraw)
            {
                state.Draw(spriteBatch, gameTime);
            }
        }

    }
}
