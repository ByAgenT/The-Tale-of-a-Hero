using System;


namespace TheTaleOfAHero.Models
{
    public static class CollisionCategory
    {
        public const uint Hero = 0x1 << 1;
        public const uint Enemy = 0x1 << 2;
        public const uint Spell = 0x1 << 3;
        public const uint Platform = 0x1 << 4;
    }
}
