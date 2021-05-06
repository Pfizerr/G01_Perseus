using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    // This class might change a lot and if it grows too large, then it should have its' own file
    public class Sector
    {
        //123
        private ICollection<Entity> entities;

        public Sector()
        {
            entities = new List<Entity>();
        }

        public ICollection<Entity> Entities => entities;

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }
    }

    public class Level
    {

        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;

        private Sector[,] tiles;

        private Background background;


        // This code is only for debugging purpose
        #region
        private Texture2D tileTexture;
        private Color[,] colors;
        #endregion

        public Level(int width, int height, int tileWidth, int tileHeight)
        {
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            background = new Background();

            this.tiles = new Sector[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    this.tiles[x, y] = new Sector();
                }
            }

            // This code is only for debugging purpose
            #region
            colors = new Color[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = (x + 1) * (255 / width);
                    int g = (y + 1) * (255 / height);
                    int b = 0;
                    int a = 255;
                    colors[x, y] = new Color(r, g, b, a);
                }
            }
            #endregion

            this.tileTexture = Util.CreateRectangleTexture(tileWidth, tileHeight, Color.Green, Color.White);
    
        }

        public void AddEntity(Entity entity)
        {
            int startX = (int)Math.Floor((entity.Position.X - (entity.Size.X * 0.5f)) / (float)tileWidth);
            int startY = (int)Math.Floor((entity.Position.Y - (entity.Size.Y * 0.5f)) / (float)tileHeight);
            int endX = (int)Math.Ceiling((entity.Position.X + (entity.Size.X * 0.5f)) / (float)tileWidth);
            int endY = (int)Math.Ceiling((entity.Position.Y + (entity.Size.Y * 0.5f)) / (float)tileHeight);

            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    Point sector = GetSectorCoordinates(x, y);

                    tiles[sector.X, sector.Y].AddEntity(entity);
                    //Console.WriteLine("Added entity: (" + sector.X + ", " + sector.Y + ")");
                }
            }
        }



        public Point GetSectorCoordinates(int x, int y)
        {
            int ix = x % width >= 0 ? x % width : (x % width) + width;
            int iy = y % height >= 0 ? y % height : (y % height) + height;
            return new Point(ix, iy);
        }


        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            //background.Draw(spriteBatch);

            int tilesPerCameraX = (int)Math.Ceiling(camera.Viewport.Width / (float)tileWidth);
            int tilesPerCameraY = (int)Math.Ceiling(camera.Viewport.Height / (float)tileHeight);

            int startX = (int)Math.Floor((camera.CenterPosition.X - (tilesPerCameraX * tileWidth * 0.5f)) / tileWidth);
            int startY = (int)Math.Floor((camera.CenterPosition.Y - (tilesPerCameraY * tileHeight * 0.5f)) / tileHeight);
            int endX = startX + (int)(tilesPerCameraX) + 1;
            int endY = startY + (int)(tilesPerCameraY) + 1;

            //Console.WriteLine(startY);

            List<Entity> drawnEntities = new List<Entity>();

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    // This code is only for debugging purpose
                    Point sector = GetSectorCoordinates(x, y);
                    #region
                    Color color = colors[sector.X, sector.Y];
                    #endregion

                    spriteBatch.Draw(tileTexture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                    //background.Draw(spriteBatch, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));

                    foreach (Entity entity in tiles[sector.X, sector.Y].Entities)
                    {
                        //if (!drawnEntities.Contains(entity))
                        {
                            Vector2 tilePosition = new Vector2(x * tileWidth, y * tileHeight);
                            Vector2 offset = new Vector2();
                            //entity.Draw(spriteBatch, x, y, sector.X, sector.Y, tileWidth, tileHeight);
                            drawnEntities.Add(entity);
                        }
                    }
                }
            }

        }
    }
}
