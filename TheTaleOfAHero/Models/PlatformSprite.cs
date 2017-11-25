using System;
using SpriteKit;
using CoreGraphics;

namespace TheTaleOfAHero.Models
{
    public class PlatformSprite : SKSpriteNode
    {

        const string LONG_PLATFORM_IMAGE = "";
        const string MEDIUM_PLATFORM_IMAGE = "";
        const string SHORT_PLATFORM_IMAGE = "ShortPlatform.png";

        public PlatformSprite(PlatformType type) : base()
        {
            switch(type) {
                case PlatformType.Short:
                    Texture = SKTexture.FromImageNamed(SHORT_PLATFORM_IMAGE);
                    break;
                case PlatformType.Medium:
                    Texture = SKTexture.FromImageNamed(MEDIUM_PLATFORM_IMAGE);
                    break;
                case PlatformType.Long:
                    Texture = SKTexture.FromImageNamed(LONG_PLATFORM_IMAGE);
                    break;
            }
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.Dynamic = false;
            PhysicsBody.CategoryBitMask = CollisionCategory.Platform;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Hero | CollisionCategory.Enemy;
            //PhysicsBody.CollisionBitMask = 0;
        }

        public static PlatformSprite CreatePlatformAt(PlatformType type, CGPoint point) {
            return new PlatformSprite(type)
            {
                Position = point
            };
        }
    }
}
