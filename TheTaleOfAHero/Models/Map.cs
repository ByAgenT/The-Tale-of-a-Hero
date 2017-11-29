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
        public HeroSprite Hero { get; set; }


        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Enemies = new List<EnemySprite>();
            Platforms = new List<PlatformSprite>();
        }

        public static Map CreateMapFromFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
