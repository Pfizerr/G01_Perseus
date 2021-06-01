using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public class Faction
    {
        private Sprite planetHighlightSprite;
        private ICollection<Planet> planets; // Planets associated with this faction
        private Sprite planetSprite;
        private string name;

        public Faction(string name, Sprite planetSprite, Sprite planetHighlightSprite)
        {
            this.planetHighlightSprite = planetHighlightSprite;
            this.planetSprite = planetSprite;
            this.name = name;
            
            planets = new List<Planet>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(Planet planet in planets)
            {
                planet.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Planet planet in planets)
            {
                planet.Draw(spriteBatch);
            }
        }

        public void CreatePlanet(string name, Vector2 position, int nrOfMissions)
        {
            planets.Add(new Planet(name, nrOfMissions, planetHighlightSprite, planetSprite, position));
        }
    }
}