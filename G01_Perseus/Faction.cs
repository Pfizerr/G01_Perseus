using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace G01_Perseus
{
    public class Faction
    {
        private string name;
        private ICollection<int> sectors; //sectors assigned to faction insteadTexture?
        private Texture2D planetHighlightTexture;

        public Faction(string name)
        {
            this.name = name;
        }

        public Texture2D PlanetHighlightTexture
        {
            get { return planetHighlightTexture; }
            set { planetHighlightTexture = value;}
        }
    }
}