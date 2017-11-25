using System;
using System.Collections.Generic;

namespace TheTaleOfAHero.Models
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<EnemySprite> Enemies { get; set; }
        public List<PlatformSprite> Platforms { get; set; }


        public Map()
        {
        }

    }
}
