using System;
using System.IO;
using System.Collections.Generic;
using CoreGraphics;


namespace TheTaleOfAHero.Models
{

    /// <summary>
    /// Map class representing all objects that should be rendered on the scene.
    /// </summary>
    public class Map
    {

        const string MAP_FILE_META = "THE TALE OF A HERO MAP";

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


        /// <summary>
        /// Creates the game map from file.
        /// </summary>
        /// <returns>Map object</returns>
        /// <param name="filename">Map file name.</param>
        public static Map CreateMapFromFile(string filename)
        {
            var resultMap = new Map(4800, 1000);

            var map = File.ReadAllLines(filename, System.Text.Encoding.Default);
            if(!(map[0] == MAP_FILE_META))
            {
                throw new IncorrectMapException("Map meta not found");
            }
            foreach(var item in map)
            {
                var mapItem = item.Split(' ');
                if(mapItem.Length == 0)
                {
                    throw new IncorrectMapException("Map definition is incorrect");    
                }
                switch(mapItem[0])
                {
                    case "HERO":
                        {
                            var xcoord = Convert.ToDouble(mapItem[1]);
                            var ycoord = Convert.ToDouble(mapItem[2]);
                            resultMap.Hero = HeroSprite.CreateHeroAt(new CGPoint(xcoord, ycoord));
                            break;
                        }

                    case "PLATFORM":
                        {
                            var type = (PlatformType)Convert.ToInt32(mapItem[1]);
                            var xcoord = Convert.ToDouble(mapItem[2]);
                            var ycoord = Convert.ToDouble(mapItem[3]);
                            resultMap.Platforms.Add(PlatformSprite.CreatePlatformAt(type, new CGPoint(xcoord, ycoord)));
                            break; 
                        }

                    case "ENEMY":
                        {
                            var xcoord = Convert.ToDouble(mapItem[1]);
                            var ycoord = Convert.ToDouble(mapItem[2]);
                            resultMap.Enemies.Add(EnemySprite.CreateEnemyAt(new CGPoint(xcoord, ycoord)));
                            break;
                        }
                }
            }
            return resultMap;
        }
    }

    /// <summary>
    /// Incorrect map exception.
    /// </summary>
    public class IncorrectMapException : Exception
    {
        public IncorrectMapException()
        {
            
        }

        public IncorrectMapException(string description) : base(description)
        {
            
        }

        public IncorrectMapException(string description, Exception inner) : base(description, inner)
        {
            
        }
    }
}
