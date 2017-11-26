using System;


namespace TheTaleOfAHero.Models
{

    /// <summary>
    /// Collision category.
    /// 
    /// Class contains bitmasks for built-in 
    /// SpriteKit collision detection system
    /// </summary>
    public static class CollisionCategory
    {
        public const uint Hero = 0x1 << 1;
        public const uint Enemy = 0x1 << 2;
        public const uint Spell = 0x1 << 3;
        public const uint Platform = 0x1 << 4;
    }
}
